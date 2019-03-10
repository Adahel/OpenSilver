
//===============================================================================
//
//  IMPORTANT NOTICE, PLEASE READ CAREFULLY:
//
//  ? This code is dual-licensed (GPLv3 + Commercial). Commercial licenses can be obtained from: http://cshtml5.com
//
//  ? You are NOT allowed to:
//       � Use this code in a proprietary or closed-source project (unless you have obtained a commercial license)
//       � Mix this code with non-GPL-licensed code (such as MIT-licensed code), or distribute it under a different license
//       � Remove or modify this notice
//
//  ? Copyright 2019 Userware/CSHTML5. This code is part of the CSHTML5 product.
//
//===============================================================================


using CSHTML5;
using System;

namespace TypeScriptDefinitionsSupport
{
    public abstract class UnionType : JSObject
    {
        protected Type _Type { get; set; }

        public bool instanceof(string type)
        {
            return Convert.ToBoolean(Interop.ExecuteJavaScript("$0 instanceof $1", this.UnderlyingJSInstance, type));
        }

        public T CreateInstance<T>()
        {
            if (typeof(T) == typeof(double))
                return (T)(object)Convert.ToDouble(this.UnderlyingJSInstance);
            else if (typeof(T) == typeof(string))
                return (T)(object)Convert.ToString(this.UnderlyingJSInstance);
            else if (typeof(T) == typeof(bool))
                return (T)(object)Convert.ToBoolean(this.UnderlyingJSInstance);
            else if (typeof(T) == typeof(object))
                return (T)this.UnderlyingJSInstance;
            else
                return (T)Activator.CreateInstance(typeof(T), this.UnderlyingJSInstance);
        }
    }

    public class UnionType<T0, T1> : UnionType
    {
        private UnionType(object jsObj)
        {
            //System.Diagnostics.Debug.WriteLine("ctor(object)");
            this.UnderlyingJSInstance = jsObj;
        }

        public static UnionType<T0, T1> FromJavaScriptInstance(object jsObj)
        {
            //System.Diagnostics.Debug.WriteLine("FromJS()");
            return new UnionType<T0, T1>(jsObj);
        }
        private T0 _t0 { get; set; }

        public UnionType(T0 t)
        {
            this._t0 = t;
            this._Type = typeof(T0);
            //System.Diagnostics.Debug.WriteLine("ctor " + typeof(UnionType<T0, T1>).Name + "<" + typeof(T0).Name + ", " + typeof(T1).Name + ">" + "(" + this._Type.Name +")");
            JSObject o = JSObject.CreateFrom(t);;
            this.UnderlyingJSInstance = o.UnderlyingJSInstance;
        }

        static public implicit operator UnionType<T0, T1>(T0 t)
        {
            //System.Diagnostics.Debug.WriteLine("cast " + typeof(UnionType<T0, T1>).Name + "<" + typeof(T0).Name + ", " + typeof(T1).Name + ">" + "(" + typeof(T0).Name + ")");
            return new UnionType<T0, T1>(t);
        }

        static public implicit operator T0(UnionType<T0, T1> value)
        {
            //System.Diagnostics.Debug.WriteLine("cast " + typeof(T0).Name + "(" + typeof(UnionType<T0, T1>).Name + "<" + typeof(T0).Name + ", " + typeof(T1).Name + ">" + ")");
            if (value._Type == null)
            {
                value._t0 = value.CreateInstance<T0>();
                value._Type = typeof(T0);
                return value._t0;
            }
            if (value._Type == typeof(T0))
                return value._t0;
            else
                throw new Exception("Unable to cast this UnionType to " + typeof(T0).Name + " because it contains a value that is of another type.");
        }
        private T1 _t1 { get; set; }

        public UnionType(T1 t)
        {
            this._t1 = t;
            this._Type = typeof(T1);
            //System.Diagnostics.Debug.WriteLine("ctor " + typeof(UnionType<T0, T1>).Name + "<" + typeof(T0).Name + ", " + typeof(T1).Name + ">" + "(" + this._Type.Name + ")");
            JSObject o = JSObject.CreateFrom(t);;
            this.UnderlyingJSInstance = o.UnderlyingJSInstance;
        }

        static public implicit operator UnionType<T0, T1>(T1 t)
        {
            //System.Diagnostics.Debug.WriteLine("cast " + typeof(UnionType<T0, T1>).Name + "<" + typeof(T0).Name + ", " + typeof(T1).Name + ">" + "(" + typeof(T0).Name + ")");
            return new UnionType<T0, T1>(t);
        }

        static public implicit operator T1(UnionType<T0, T1> value)
        {
            //System.Diagnostics.Debug.WriteLine("cast " + typeof(T1).Name + "(" + typeof(UnionType<T0, T1>).Name + "<" + typeof(T0).Name + ", " + typeof(T1).Name + ">" + ")");
            if (value._Type == null)
            {
                value._t1 = value.CreateInstance<T1>();
                value._Type = typeof(T1);
                return value._t1;
            }
            if (value._Type == typeof(T1))
                return value._t1;
            else
                throw new Exception("Unable to cast this UnionType to " + typeof(T1).Name + " because it contains a value that is of another type.");
        }
    }

    public class UnionType<T0, T1, T2> : UnionType
    {
        private UnionType(object jsObj)
        {
            this.UnderlyingJSInstance = jsObj;
        }

        public static UnionType<T0, T1, T2> FromJavaScriptInstance(object jsObj)
        {
            return new UnionType<T0, T1, T2>(jsObj);
        }
        private T0 _t0 { get; set; }

        public UnionType(T0 t)
        {
            this._t0 = t;
            this._Type = typeof(T0);
            JSObject o = JSObject.CreateFrom(t);;
            this.UnderlyingJSInstance = o.UnderlyingJSInstance;
        }

        static public implicit operator UnionType<T0, T1, T2>(T0 t)
        {
            return new UnionType<T0, T1, T2>(t);
        }

        static public implicit operator T0(UnionType<T0, T1, T2> value)
        {
            if (value._Type == null)
            {
                value._t0 = value.CreateInstance<T0>();
                value._Type = typeof(T0);
                return value._t0;
            }
            if (value._Type == typeof(T0))
                return value._t0;
            else
                throw new Exception("Unable to cast this UnionType to " + typeof(T0).Name + " because it contains a value that is of another type.");
        }
        private T1 _t1 { get; set; }

        public UnionType(T1 t)
        {
            this._t1 = t;
            this._Type = typeof(T1);
            JSObject o = JSObject.CreateFrom(t);;
            this.UnderlyingJSInstance = o.UnderlyingJSInstance;
        }

        static public implicit operator UnionType<T0, T1, T2>(T1 t)
        {
            return new UnionType<T0, T1, T2>(t);
        }

        static public implicit operator T1(UnionType<T0, T1, T2> value)
        {
            if (value._Type == null)
            {
                value._t1 = value.CreateInstance<T1>();
                value._Type = typeof(T1);
                return value._t1;
            }
            if (value._Type == typeof(T1))
                return value._t1;
            else
                throw new Exception("Unable to cast this UnionType to " + typeof(T1).Name + " because it contains a value that is of another type.");
        }
        private T2 _t2 { get; set; }

        public UnionType(T2 t)
        {
            this._t2 = t;
            this._Type = typeof(T2);
            JSObject o = JSObject.CreateFrom(t);;
            this.UnderlyingJSInstance = o.UnderlyingJSInstance;
        }

        static public implicit operator UnionType<T0, T1, T2>(T2 t)
        {
            return new UnionType<T0, T1, T2>(t);
        }

        static public implicit operator T2(UnionType<T0, T1, T2> value)
        {
            if (value._Type == null)
            {
                value._t2 = value.CreateInstance<T2>();
                value._Type = typeof(T2);
                return value._t2;
            }
            if (value._Type == typeof(T2))
                return value._t2;
            else
                throw new Exception("Unable to cast this UnionType to " + typeof(T2).Name + " because it contains a value that is of another type.");
        }
    }

    public class UnionType<T0, T1, T2, T3> : UnionType
    {
        private UnionType(object jsObj)
        {
            this.UnderlyingJSInstance = jsObj;
        }

        public static UnionType<T0, T1, T2, T3> FromJavaScriptInstance(object jsObj)
        {
            return new UnionType<T0, T1, T2, T3>(jsObj);
        }
        private T0 _t0 { get; set; }

        public UnionType(T0 t)
        {
            this._t0 = t;
            this._Type = typeof(T0);
            JSObject o = JSObject.CreateFrom(t);;
            this.UnderlyingJSInstance = o.UnderlyingJSInstance;
        }

        static public implicit operator UnionType<T0, T1, T2, T3>(T0 t)
        {
            return new UnionType<T0, T1, T2, T3>(t);
        }

        static public implicit operator T0(UnionType<T0, T1, T2, T3> value)
        {
            if (value._Type == null)
            {
                value._t0 = value.CreateInstance<T0>();
                value._Type = typeof(T0);
                return value._t0;
            }
            if (value._Type == typeof(T0))
                return value._t0;
            else
                throw new Exception("Unable to cast this UnionType to " + typeof(T0).Name + " because it contains a value that is of another type.");
        }
        private T1 _t1 { get; set; }

        public UnionType(T1 t)
        {
            this._t1 = t;
            this._Type = typeof(T1);
            JSObject o = JSObject.CreateFrom(t);;
            this.UnderlyingJSInstance = o.UnderlyingJSInstance;
        }

        static public implicit operator UnionType<T0, T1, T2, T3>(T1 t)
        {
            return new UnionType<T0, T1, T2, T3>(t);
        }

        static public implicit operator T1(UnionType<T0, T1, T2, T3> value)
        {
            if (value._Type == null)
            {
                value._t1 = value.CreateInstance<T1>();
                value._Type = typeof(T1);
                return value._t1;
            }
            if (value._Type == typeof(T1))
                return value._t1;
            else
                throw new Exception("Unable to cast this UnionType to " + typeof(T1).Name + " because it contains a value that is of another type.");
        }
        private T2 _t2 { get; set; }

        public UnionType(T2 t)
        {
            this._t2 = t;
            this._Type = typeof(T2);
            JSObject o = JSObject.CreateFrom(t);;
            this.UnderlyingJSInstance = o.UnderlyingJSInstance;
        }

        static public implicit operator UnionType<T0, T1, T2, T3>(T2 t)
        {
            return new UnionType<T0, T1, T2, T3>(t);
        }

        static public implicit operator T2(UnionType<T0, T1, T2, T3> value)
        {
            if (value._Type == null)
            {
                value._t2 = value.CreateInstance<T2>();
                value._Type = typeof(T2);
                return value._t2;
            }
            if (value._Type == typeof(T2))
                return value._t2;
            else
                throw new Exception("Unable to cast this UnionType to " + typeof(T2).Name + " because it contains a value that is of another type.");
        }
        private T3 _t3 { get; set; }

        public UnionType(T3 t)
        {
            this._t3 = t;
            this._Type = typeof(T3);
            JSObject o = JSObject.CreateFrom(t);;
            this.UnderlyingJSInstance = o.UnderlyingJSInstance;
        }

        static public implicit operator UnionType<T0, T1, T2, T3>(T3 t)
        {
            return new UnionType<T0, T1, T2, T3>(t);
        }

        static public implicit operator T3(UnionType<T0, T1, T2, T3> value)
        {
            if (value._Type == null)
            {
                value._t3 = value.CreateInstance<T3>();
                value._Type = typeof(T3);
                return value._t3;
            }
            if (value._Type == typeof(T3))
                return value._t3;
            else
                throw new Exception("Unable to cast this UnionType to " + typeof(T3).Name + " because it contains a value that is of another type.");
        }
    }

    public class UnionType<T0, T1, T2, T3, T4> : UnionType
    {
        private UnionType(object jsObj)
        {
            this.UnderlyingJSInstance = jsObj;
        }

        public static UnionType<T0, T1, T2, T3, T4> FromJavaScriptInstance(object jsObj)
        {
            return new UnionType<T0, T1, T2, T3, T4>(jsObj);
        }
        private T0 _t0 { get; set; }

        public UnionType(T0 t)
        {
            this._t0 = t;
            this._Type = typeof(T0);
            JSObject o = JSObject.CreateFrom(t);;
            this.UnderlyingJSInstance = o.UnderlyingJSInstance;
        }

        static public implicit operator UnionType<T0, T1, T2, T3, T4>(T0 t)
        {
            return new UnionType<T0, T1, T2, T3, T4>(t);
        }

        static public implicit operator T0(UnionType<T0, T1, T2, T3, T4> value)
        {
            if (value._Type == null)
            {
                value._t0 = value.CreateInstance<T0>();
                value._Type = typeof(T0);
                return value._t0;
            }
            if (value._Type == typeof(T0))
                return value._t0;
            else
                throw new Exception("Unable to cast this UnionType to " + typeof(T0).Name + " because it contains a value that is of another type.");
        }
        private T1 _t1 { get; set; }

        public UnionType(T1 t)
        {
            this._t1 = t;
            this._Type = typeof(T1);
            JSObject o = JSObject.CreateFrom(t);;
            this.UnderlyingJSInstance = o.UnderlyingJSInstance;
        }

        static public implicit operator UnionType<T0, T1, T2, T3, T4>(T1 t)
        {
            return new UnionType<T0, T1, T2, T3, T4>(t);
        }

        static public implicit operator T1(UnionType<T0, T1, T2, T3, T4> value)
        {
            if (value._Type == null)
            {
                value._t1 = value.CreateInstance<T1>();
                value._Type = typeof(T1);
                return value._t1;
            }
            if (value._Type == typeof(T1))
                return value._t1;
            else
                throw new Exception("Unable to cast this UnionType to " + typeof(T1).Name + " because it contains a value that is of another type.");
        }
        private T2 _t2 { get; set; }

        public UnionType(T2 t)
        {
            this._t2 = t;
            this._Type = typeof(T2);
            JSObject o = JSObject.CreateFrom(t);;
            this.UnderlyingJSInstance = o.UnderlyingJSInstance;
        }

        static public implicit operator UnionType<T0, T1, T2, T3, T4>(T2 t)
        {
            return new UnionType<T0, T1, T2, T3, T4>(t);
        }

        static public implicit operator T2(UnionType<T0, T1, T2, T3, T4> value)
        {
            if (value._Type == null)
            {
                value._t2 = value.CreateInstance<T2>();
                value._Type = typeof(T2);
                return value._t2;
            }
            if (value._Type == typeof(T2))
                return value._t2;
            else
                throw new Exception("Unable to cast this UnionType to " + typeof(T2).Name + " because it contains a value that is of another type.");
        }
        private T3 _t3 { get; set; }

        public UnionType(T3 t)
        {
            this._t3 = t;
            this._Type = typeof(T3);
            JSObject o = JSObject.CreateFrom(t);;
            this.UnderlyingJSInstance = o.UnderlyingJSInstance;
        }

        static public implicit operator UnionType<T0, T1, T2, T3, T4>(T3 t)
        {
            return new UnionType<T0, T1, T2, T3, T4>(t);
        }

        static public implicit operator T3(UnionType<T0, T1, T2, T3, T4> value)
        {
            if (value._Type == null)
            {
                value._t3 = value.CreateInstance<T3>();
                value._Type = typeof(T3);
                return value._t3;
            }
            if (value._Type == typeof(T3))
                return value._t3;
            else
                throw new Exception("Unable to cast this UnionType to " + typeof(T3).Name + " because it contains a value that is of another type.");
        }
        private T4 _t4 { get; set; }

        public UnionType(T4 t)
        {
            this._t4 = t;
            this._Type = typeof(T4);
            JSObject o = JSObject.CreateFrom(t);;
            this.UnderlyingJSInstance = o.UnderlyingJSInstance;
        }

        static public implicit operator UnionType<T0, T1, T2, T3, T4>(T4 t)
        {
            return new UnionType<T0, T1, T2, T3, T4>(t);
        }

        static public implicit operator T4(UnionType<T0, T1, T2, T3, T4> value)
        {
            if (value._Type == null)
            {
                value._t4 = value.CreateInstance<T4>();
                value._Type = typeof(T4);
                return value._t4;
            }
            if (value._Type == typeof(T4))
                return value._t4;
            else
                throw new Exception("Unable to cast this UnionType to " + typeof(T4).Name + " because it contains a value that is of another type.");
        }
    }

}