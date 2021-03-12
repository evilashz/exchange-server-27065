using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000728 RID: 1832
	[Serializable]
	internal sealed class SafeSerializationManager : IObjectReference, ISerializable
	{
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060051B2 RID: 20914 RVA: 0x0011E5C4 File Offset: 0x0011C7C4
		// (remove) Token: 0x060051B3 RID: 20915 RVA: 0x0011E5FC File Offset: 0x0011C7FC
		internal event EventHandler<SafeSerializationEventArgs> SerializeObjectState;

		// Token: 0x060051B4 RID: 20916 RVA: 0x0011E631 File Offset: 0x0011C831
		internal SafeSerializationManager()
		{
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x0011E63C File Offset: 0x0011C83C
		[SecurityCritical]
		private SafeSerializationManager(SerializationInfo info, StreamingContext context)
		{
			RuntimeType runtimeType = info.GetValueNoThrow("CLR_SafeSerializationManager_RealType", typeof(RuntimeType)) as RuntimeType;
			if (runtimeType == null)
			{
				this.m_serializedStates = (info.GetValue("m_serializedStates", typeof(List<object>)) as List<object>);
				return;
			}
			this.m_realType = runtimeType;
			this.m_savedSerializationInfo = info;
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x060051B6 RID: 20918 RVA: 0x0011E6A2 File Offset: 0x0011C8A2
		internal bool IsActive
		{
			get
			{
				return this.SerializeObjectState != null;
			}
		}

		// Token: 0x060051B7 RID: 20919 RVA: 0x0011E6B0 File Offset: 0x0011C8B0
		[SecurityCritical]
		internal void CompleteSerialization(object serializedObject, SerializationInfo info, StreamingContext context)
		{
			this.m_serializedStates = null;
			EventHandler<SafeSerializationEventArgs> serializeObjectState = this.SerializeObjectState;
			if (serializeObjectState != null)
			{
				SafeSerializationEventArgs safeSerializationEventArgs = new SafeSerializationEventArgs(context);
				serializeObjectState(serializedObject, safeSerializationEventArgs);
				this.m_serializedStates = safeSerializationEventArgs.SerializedStates;
				info.AddValue("CLR_SafeSerializationManager_RealType", serializedObject.GetType(), typeof(RuntimeType));
				info.SetType(typeof(SafeSerializationManager));
			}
		}

		// Token: 0x060051B8 RID: 20920 RVA: 0x0011E714 File Offset: 0x0011C914
		internal void CompleteDeserialization(object deserializedObject)
		{
			if (this.m_serializedStates != null)
			{
				foreach (object obj in this.m_serializedStates)
				{
					ISafeSerializationData safeSerializationData = (ISafeSerializationData)obj;
					safeSerializationData.CompleteDeserialization(deserializedObject);
				}
			}
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x0011E770 File Offset: 0x0011C970
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_serializedStates", this.m_serializedStates, typeof(List<IDeserializationCallback>));
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x0011E790 File Offset: 0x0011C990
		[SecurityCritical]
		object IObjectReference.GetRealObject(StreamingContext context)
		{
			if (this.m_realObject != null)
			{
				return this.m_realObject;
			}
			if (this.m_realType == null)
			{
				return this;
			}
			Stack stack = new Stack();
			RuntimeType runtimeType = this.m_realType;
			do
			{
				stack.Push(runtimeType);
				runtimeType = (runtimeType.BaseType as RuntimeType);
			}
			while (runtimeType != typeof(object));
			RuntimeType t;
			RuntimeConstructorInfo runtimeConstructorInfo;
			do
			{
				t = runtimeType;
				runtimeType = (stack.Pop() as RuntimeType);
				runtimeConstructorInfo = runtimeType.GetSerializationCtor();
			}
			while (runtimeConstructorInfo != null && runtimeConstructorInfo.IsSecurityCritical);
			runtimeConstructorInfo = ObjectManager.GetConstructor(t);
			object uninitializedObject = FormatterServices.GetUninitializedObject(this.m_realType);
			runtimeConstructorInfo.SerializationInvoke(uninitializedObject, this.m_savedSerializationInfo, context);
			this.m_savedSerializationInfo = null;
			this.m_realType = null;
			this.m_realObject = uninitializedObject;
			return uninitializedObject;
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x0011E854 File Offset: 0x0011CA54
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this.m_realObject != null)
			{
				SerializationEvents serializationEventsForType = SerializationEventsCache.GetSerializationEventsForType(this.m_realObject.GetType());
				serializationEventsForType.InvokeOnDeserialized(this.m_realObject, context);
				this.m_realObject = null;
			}
		}

		// Token: 0x040023FC RID: 9212
		private IList<object> m_serializedStates;

		// Token: 0x040023FD RID: 9213
		private SerializationInfo m_savedSerializationInfo;

		// Token: 0x040023FE RID: 9214
		private object m_realObject;

		// Token: 0x040023FF RID: 9215
		private RuntimeType m_realType;

		// Token: 0x04002401 RID: 9217
		private const string RealTypeSerializationName = "CLR_SafeSerializationManager_RealType";
	}
}
