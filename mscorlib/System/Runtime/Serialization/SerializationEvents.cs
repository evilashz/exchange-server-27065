using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x0200072A RID: 1834
	internal class SerializationEvents
	{
		// Token: 0x060051C0 RID: 20928 RVA: 0x0011E950 File Offset: 0x0011CB50
		private List<MethodInfo> GetMethodsWithAttribute(Type attribute, Type t)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			Type type = t;
			while (type != null && type != typeof(object))
			{
				RuntimeType runtimeType = (RuntimeType)type;
				MethodInfo[] methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (MethodInfo methodInfo in methods)
				{
					if (methodInfo.IsDefined(attribute, false))
					{
						list.Add(methodInfo);
					}
				}
				type = type.BaseType;
			}
			list.Reverse();
			if (list.Count != 0)
			{
				return list;
			}
			return null;
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x0011E9DC File Offset: 0x0011CBDC
		internal SerializationEvents(Type t)
		{
			this.m_OnSerializingMethods = this.GetMethodsWithAttribute(typeof(OnSerializingAttribute), t);
			this.m_OnSerializedMethods = this.GetMethodsWithAttribute(typeof(OnSerializedAttribute), t);
			this.m_OnDeserializingMethods = this.GetMethodsWithAttribute(typeof(OnDeserializingAttribute), t);
			this.m_OnDeserializedMethods = this.GetMethodsWithAttribute(typeof(OnDeserializedAttribute), t);
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x060051C2 RID: 20930 RVA: 0x0011EA4B File Offset: 0x0011CC4B
		internal bool HasOnSerializingEvents
		{
			get
			{
				return this.m_OnSerializingMethods != null || this.m_OnSerializedMethods != null;
			}
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x0011EA60 File Offset: 0x0011CC60
		[SecuritySafeCritical]
		internal void InvokeOnSerializing(object obj, StreamingContext context)
		{
			if (this.m_OnSerializingMethods != null)
			{
				object[] array = new object[]
				{
					context
				};
				SerializationEventHandler serializationEventHandler = null;
				foreach (MethodInfo method in this.m_OnSerializingMethods)
				{
					SerializationEventHandler b = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, method);
					serializationEventHandler = (SerializationEventHandler)Delegate.Combine(serializationEventHandler, b);
				}
				serializationEventHandler(context);
			}
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x0011EAF8 File Offset: 0x0011CCF8
		[SecuritySafeCritical]
		internal void InvokeOnDeserializing(object obj, StreamingContext context)
		{
			if (this.m_OnDeserializingMethods != null)
			{
				object[] array = new object[]
				{
					context
				};
				SerializationEventHandler serializationEventHandler = null;
				foreach (MethodInfo method in this.m_OnDeserializingMethods)
				{
					SerializationEventHandler b = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, method);
					serializationEventHandler = (SerializationEventHandler)Delegate.Combine(serializationEventHandler, b);
				}
				serializationEventHandler(context);
			}
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x0011EB90 File Offset: 0x0011CD90
		[SecuritySafeCritical]
		internal void InvokeOnDeserialized(object obj, StreamingContext context)
		{
			if (this.m_OnDeserializedMethods != null)
			{
				object[] array = new object[]
				{
					context
				};
				SerializationEventHandler serializationEventHandler = null;
				foreach (MethodInfo method in this.m_OnDeserializedMethods)
				{
					SerializationEventHandler b = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, method);
					serializationEventHandler = (SerializationEventHandler)Delegate.Combine(serializationEventHandler, b);
				}
				serializationEventHandler(context);
			}
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x0011EC28 File Offset: 0x0011CE28
		[SecurityCritical]
		internal SerializationEventHandler AddOnSerialized(object obj, SerializationEventHandler handler)
		{
			if (this.m_OnSerializedMethods != null)
			{
				foreach (MethodInfo method in this.m_OnSerializedMethods)
				{
					SerializationEventHandler b = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, method);
					handler = (SerializationEventHandler)Delegate.Combine(handler, b);
				}
			}
			return handler;
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x0011ECA8 File Offset: 0x0011CEA8
		[SecurityCritical]
		internal SerializationEventHandler AddOnDeserialized(object obj, SerializationEventHandler handler)
		{
			if (this.m_OnDeserializedMethods != null)
			{
				foreach (MethodInfo method in this.m_OnDeserializedMethods)
				{
					SerializationEventHandler b = (SerializationEventHandler)Delegate.CreateDelegateNoSecurityCheck((RuntimeType)typeof(SerializationEventHandler), obj, method);
					handler = (SerializationEventHandler)Delegate.Combine(handler, b);
				}
			}
			return handler;
		}

		// Token: 0x04002405 RID: 9221
		private List<MethodInfo> m_OnSerializingMethods;

		// Token: 0x04002406 RID: 9222
		private List<MethodInfo> m_OnSerializedMethods;

		// Token: 0x04002407 RID: 9223
		private List<MethodInfo> m_OnDeserializingMethods;

		// Token: 0x04002408 RID: 9224
		private List<MethodInfo> m_OnDeserializedMethods;
	}
}
