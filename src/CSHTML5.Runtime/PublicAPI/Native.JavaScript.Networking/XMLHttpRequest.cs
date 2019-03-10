﻿
//===============================================================================
//
//  IMPORTANT NOTICE, PLEASE READ CAREFULLY:
//
//  ● This code is dual-licensed (GPLv3 + Commercial). Commercial licenses can be obtained from: http://cshtml5.com
//
//  ● You are NOT allowed to:
//       – Use this code in a proprietary or closed-source project (unless you have obtained a commercial license)
//       – Mix this code with non-GPL-licensed code (such as MIT-licensed code), or distribute it under a different license
//       – Remove or modify this notice
//
//  ● Copyright 2019 Userware/CSHTML5. This code is part of the CSHTML5 product.
//
//===============================================================================


using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSHTML5.Native.JavaScript.Networking
{
    //todo: implement cors (see jsil doxhr)
    //todo: binary response

    internal enum HttpMethod
    {
        GET, POST, PUT, DELETE
    }

    internal enum XhrReadyState
    {
        UNSENT,             //0: Client has been created. open() not called yet.
        OPENED,             //1: open() has been called.
        HEADERS_RECEIVED,   //2: send() has been called, and headers and status are available.
        LOADING,	        //3: Downloading; responseText holds partial data.
        DONE,               //4: The operation is complete.
    }

    public class XMLHttpRequest
    {
        private const int _defaultTimeOut = 2000;

        private TaskCompletionSource<string> _tcs;
        private object _xmlHttpRequest;

        public Uri Uri { get; set; }
        public string Method { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }

        public XMLHttpRequest()
        {
            this.Method = HttpMethod.GET.ToString();
            this.Headers = new Dictionary<string, string>();
        }

        public XMLHttpRequest(Uri uri)
            : this()
        {
            this.Uri = uri;
        }

        public XMLHttpRequest(string address)
            : this()
        {
            this.Uri = new Uri(address);
        }

        public string Execute()
        {
            object xhr = NewHttpRequest();

            Open(xhr, this.Method, this.Uri.ToString(), isAsync: false);
            foreach (var kv in this.Headers)
                SetRequestHeader(xhr, kv.Key, kv.Value);

            Send(xhr, this.Body);

            CheckStatusAndThrow(xhr);

            object responseObject = GetResponse(xhr);
            return responseObject != null ? responseObject.ToString() : null;
        }

        public Task<string> ExecuteAsync(int timeOut = _defaultTimeOut)
        {
            _tcs = new TaskCompletionSource<string>();

            _xmlHttpRequest = NewHttpRequest();

            OnLoad(_xmlHttpRequest, InternalOnLoad);
            OnProgress(_xmlHttpRequest, InternalOnProgress);
            OnError(_xmlHttpRequest, InternalOnError);
            OnAbort(_xmlHttpRequest, InternalOnAbort);
            OnTimeOut(_xmlHttpRequest, InternalOnTimeOut);

            Open(_xmlHttpRequest, this.Method, this.Uri.ToString(), isAsync: true);
            foreach (var kv in this.Headers)
                SetRequestHeader(_xmlHttpRequest, kv.Key, kv.Value);

            SetTimout(_xmlHttpRequest, timeOut);
            Send(_xmlHttpRequest, this.Body);

            return _tcs.Task;
        }

        private void InternalOnProgress(object e)
        {
            //TODO
        }

        private void InternalOnLoad(object e)
        {
            var exception = CheckStatus(_xmlHttpRequest);
            if (exception == null)
            {
                object responseObject = GetResponse(_xmlHttpRequest);
                var response = responseObject != null ? responseObject.ToString() : null;
                _tcs.SetResult(response);
            }
            else
            {
                _tcs.SetException(exception);
            }
        }

        private void InternalOnError(object e)
        {
            var exception = CheckStatus(_xmlHttpRequest);
            if (exception == null)
                exception = new NotSupportedException("This error type is not yet supported!");
            _tcs.SetException(exception);
        }

        private void InternalOnAbort(object e)
        {
            var exception = CheckStatus(_xmlHttpRequest);
            if (exception == null)
                exception = new NotSupportedException("This abort type is not yet supported!");
            _tcs.SetException(exception);
        }

        private void InternalOnTimeOut(object e)
        {
            var exception = new TimeoutException();
            _tcs.SetException(exception);
        }

        private void CheckStatusAndThrow(object xhr)
        {
            var exception = CheckStatus(xhr);
            if (exception != null)
                throw exception;
        }

        private Exception CheckStatus(object xhr)
        {
            //TODO: test and implement all cases
            var readState = GetReadyState(xhr);
            var status = GetStatus(xhr);
            if (status == 0)
            {
                return new Exception("no response from server or error");
            }
            if (status == 404)
            {
                return new Exception("there was a response from the server; it was a 404 meaning resource not found");
            }
            if (status >= 500)
            {
                return new Exception("there was a response from the server; it was a " + status + " meaning an error occured on the server");
            }
            if (status != 200)
            {
                return new Exception("there was a response from the server; it was a " + status);
            }
            switch (readState)
            {
                case XhrReadyState.UNSENT:
                    return new Exception("The request is not initialized!");
                case XhrReadyState.OPENED:
                    return new Exception("An error occured. Cross-Site Http Request might not be allowed at the target Url. If you own the domain of the Url, consider adding the header \"Access-Control-Allow-Origin\" to enable requests to be done at this Url.");
                case XhrReadyState.HEADERS_RECEIVED:
                case XhrReadyState.LOADING:
                    return new Exception("An error occured while submitting your request.");
                case XhrReadyState.DONE:
                    //OK
                    break;
            }
            return null;
        }

        private object NewHttpRequest()
        {
            return Interop.ExecuteJavaScript("new XMLHttpRequest()");
        }

        private void SetTimout(object xmlHttpRequest, int timeOut)
        {
            Interop.ExecuteJavaScript("$0.timeout = $1", xmlHttpRequest, timeOut);
        }

        private void SetRequestHeader(object xmlHttpRequest, string key, string header)
        {
            Interop.ExecuteJavaScript("$0.setRequestHeader($1, $2)", xmlHttpRequest, key, header);
        }

        private void OnLoad(object xmlHttpRequest, Action<object> onLoad)
        {
            Interop.ExecuteJavaScript("$0.onload = $1", xmlHttpRequest, onLoad);
        }

        private void OnProgress(object xmlHttpRequest, Action<object> onProgress)
        {
            Interop.ExecuteJavaScript("$0.onprogress = $1", xmlHttpRequest, onProgress);
        }

        private void OnError(object xmlHttpRequest, Action<object> onError)
        {
            Interop.ExecuteJavaScript("$0.onerror = $1", xmlHttpRequest, onError);
        }

        private void OnAbort(object xmlHttpRequest, Action<object> onAbort)
        {
            Interop.ExecuteJavaScript("$0.onabort = $1", xmlHttpRequest, onAbort);
        }

        private void OnTimeOut(object xmlHttpRequest, Action<object> onTimeOut)
        {
            Interop.ExecuteJavaScript("$0.ontimeout = $1", xmlHttpRequest, onTimeOut);
        }

        private void Open(object xmlHttpRequest, string method, string address, bool isAsync)
        {
            Interop.ExecuteJavaScript("$0.open($1, $2, $3)", xmlHttpRequest, method, address, isAsync);
        }

        private static void Send(object xmlHttpRequest, string body)
        {
            Interop.ExecuteJavaScript("$0.send($1)", xmlHttpRequest, body ?? "");
        }

        private static XhrReadyState GetReadyState(object xmlHttpRequest)
        {
            return (XhrReadyState)Convert.ToInt32(Interop.ExecuteJavaScript("$0.readyState", xmlHttpRequest));
        }

        private static int GetStatus(object xmlHttpRequest)
        {
            return Convert.ToInt32(Interop.ExecuteJavaScript("$0.status", xmlHttpRequest)); ;
        }

        private static string GetResponse(object xmlHttpRequest)
        {
            return Interop.ExecuteJavaScript("$0.responseText", xmlHttpRequest).ToString();
        }
    }
}
