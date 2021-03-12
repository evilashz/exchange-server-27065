using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting
{
	// Token: 0x0200078F RID: 1935
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class ObjRef : IObjectReference, ISerializable
	{
		// Token: 0x06005483 RID: 21635 RVA: 0x0012B5FA File Offset: 0x001297FA
		internal void SetServerIdentity(GCHandle hndSrvIdentity)
		{
			this.srvIdentity = hndSrvIdentity;
		}

		// Token: 0x06005484 RID: 21636 RVA: 0x0012B603 File Offset: 0x00129803
		internal GCHandle GetServerIdentity()
		{
			return this.srvIdentity;
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x0012B60B File Offset: 0x0012980B
		internal void SetDomainID(int id)
		{
			this.domainID = id;
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x0012B614 File Offset: 0x00129814
		internal int GetDomainID()
		{
			return this.domainID;
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x0012B61C File Offset: 0x0012981C
		[SecurityCritical]
		private ObjRef(ObjRef o)
		{
			this.uri = o.uri;
			this.typeInfo = o.typeInfo;
			this.envoyInfo = o.envoyInfo;
			this.channelInfo = o.channelInfo;
			this.objrefFlags = o.objrefFlags;
			this.SetServerIdentity(o.GetServerIdentity());
			this.SetDomainID(o.GetDomainID());
		}

		// Token: 0x06005488 RID: 21640 RVA: 0x0012B684 File Offset: 0x00129884
		[SecurityCritical]
		public ObjRef(MarshalByRefObject o, Type requestedType)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			RuntimeType runtimeType = requestedType as RuntimeType;
			if (requestedType != null && runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			bool flag;
			Identity identity = MarshalByRefObject.GetIdentity(o, out flag);
			this.Init(o, identity, runtimeType);
		}

		// Token: 0x06005489 RID: 21641 RVA: 0x0012B6E0 File Offset: 0x001298E0
		[SecurityCritical]
		protected ObjRef(SerializationInfo info, StreamingContext context)
		{
			string text = null;
			bool flag = false;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("uri"))
				{
					this.uri = (string)enumerator.Value;
				}
				else if (enumerator.Name.Equals("typeInfo"))
				{
					this.typeInfo = (IRemotingTypeInfo)enumerator.Value;
				}
				else if (enumerator.Name.Equals("envoyInfo"))
				{
					this.envoyInfo = (IEnvoyInfo)enumerator.Value;
				}
				else if (enumerator.Name.Equals("channelInfo"))
				{
					this.channelInfo = (IChannelInfo)enumerator.Value;
				}
				else if (enumerator.Name.Equals("objrefFlags"))
				{
					object value = enumerator.Value;
					if (value.GetType() == typeof(string))
					{
						this.objrefFlags = ((IConvertible)value).ToInt32(null);
					}
					else
					{
						this.objrefFlags = (int)value;
					}
				}
				else if (enumerator.Name.Equals("fIsMarshalled"))
				{
					object value2 = enumerator.Value;
					int num;
					if (value2.GetType() == typeof(string))
					{
						num = ((IConvertible)value2).ToInt32(null);
					}
					else
					{
						num = (int)value2;
					}
					if (num == 0)
					{
						flag = true;
					}
				}
				else if (enumerator.Name.Equals("url"))
				{
					text = (string)enumerator.Value;
				}
				else if (enumerator.Name.Equals("SrvIdentity"))
				{
					this.SetServerIdentity((GCHandle)enumerator.Value);
				}
				else if (enumerator.Name.Equals("DomainId"))
				{
					this.SetDomainID((int)enumerator.Value);
				}
			}
			if (!flag)
			{
				this.objrefFlags |= 1;
			}
			else
			{
				this.objrefFlags &= -2;
			}
			if (text != null)
			{
				this.uri = text;
				this.objrefFlags |= 4;
			}
		}

		// Token: 0x0600548A RID: 21642 RVA: 0x0012B8FC File Offset: 0x00129AFC
		[SecurityCritical]
		internal bool CanSmuggle()
		{
			if (base.GetType() != typeof(ObjRef) || this.IsObjRefLite())
			{
				return false;
			}
			Type left = null;
			if (this.typeInfo != null)
			{
				left = this.typeInfo.GetType();
			}
			Type left2 = null;
			if (this.channelInfo != null)
			{
				left2 = this.channelInfo.GetType();
			}
			if ((left == null || left == typeof(TypeInfo) || left == typeof(DynamicTypeInfo)) && this.envoyInfo == null && (left2 == null || left2 == typeof(ChannelInfo)))
			{
				if (this.channelInfo != null)
				{
					foreach (object obj in this.channelInfo.ChannelData)
					{
						if (!(obj is CrossAppDomainData))
						{
							return false;
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600548B RID: 21643 RVA: 0x0012B9DB File Offset: 0x00129BDB
		[SecurityCritical]
		internal ObjRef CreateSmuggleableCopy()
		{
			return new ObjRef(this);
		}

		// Token: 0x0600548C RID: 21644 RVA: 0x0012B9E4 File Offset: 0x00129BE4
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.SetType(ObjRef.orType);
			if (!this.IsObjRefLite())
			{
				info.AddValue("uri", this.uri, typeof(string));
				info.AddValue("objrefFlags", this.objrefFlags);
				info.AddValue("typeInfo", this.typeInfo, typeof(IRemotingTypeInfo));
				info.AddValue("envoyInfo", this.envoyInfo, typeof(IEnvoyInfo));
				info.AddValue("channelInfo", this.GetChannelInfoHelper(), typeof(IChannelInfo));
				return;
			}
			info.AddValue("url", this.uri, typeof(string));
		}

		// Token: 0x0600548D RID: 21645 RVA: 0x0012BAAC File Offset: 0x00129CAC
		[SecurityCritical]
		private IChannelInfo GetChannelInfoHelper()
		{
			ChannelInfo channelInfo = this.channelInfo as ChannelInfo;
			if (channelInfo == null)
			{
				return this.channelInfo;
			}
			object[] channelData = channelInfo.ChannelData;
			if (channelData == null)
			{
				return channelInfo;
			}
			string[] array = (string[])CallContext.GetData("__bashChannelUrl");
			if (array == null)
			{
				return channelInfo;
			}
			string value = array[0];
			string text = array[1];
			ChannelInfo channelInfo2 = new ChannelInfo();
			channelInfo2.ChannelData = new object[channelData.Length];
			for (int i = 0; i < channelData.Length; i++)
			{
				channelInfo2.ChannelData[i] = channelData[i];
				ChannelDataStore channelDataStore = channelInfo2.ChannelData[i] as ChannelDataStore;
				if (channelDataStore != null)
				{
					string[] channelUris = channelDataStore.ChannelUris;
					if (channelUris != null && channelUris.Length == 1 && channelUris[0].Equals(value))
					{
						ChannelDataStore channelDataStore2 = channelDataStore.InternalShallowCopy();
						channelDataStore2.ChannelUris = new string[1];
						channelDataStore2.ChannelUris[0] = text;
						channelInfo2.ChannelData[i] = channelDataStore2;
					}
				}
			}
			return channelInfo2;
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x0600548E RID: 21646 RVA: 0x0012BB93 File Offset: 0x00129D93
		// (set) Token: 0x0600548F RID: 21647 RVA: 0x0012BB9B File Offset: 0x00129D9B
		public virtual string URI
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06005490 RID: 21648 RVA: 0x0012BBA4 File Offset: 0x00129DA4
		// (set) Token: 0x06005491 RID: 21649 RVA: 0x0012BBAC File Offset: 0x00129DAC
		public virtual IRemotingTypeInfo TypeInfo
		{
			get
			{
				return this.typeInfo;
			}
			set
			{
				this.typeInfo = value;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06005492 RID: 21650 RVA: 0x0012BBB5 File Offset: 0x00129DB5
		// (set) Token: 0x06005493 RID: 21651 RVA: 0x0012BBBD File Offset: 0x00129DBD
		public virtual IEnvoyInfo EnvoyInfo
		{
			get
			{
				return this.envoyInfo;
			}
			set
			{
				this.envoyInfo = value;
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06005494 RID: 21652 RVA: 0x0012BBC6 File Offset: 0x00129DC6
		// (set) Token: 0x06005495 RID: 21653 RVA: 0x0012BBCE File Offset: 0x00129DCE
		public virtual IChannelInfo ChannelInfo
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.channelInfo;
			}
			set
			{
				this.channelInfo = value;
			}
		}

		// Token: 0x06005496 RID: 21654 RVA: 0x0012BBD7 File Offset: 0x00129DD7
		[SecurityCritical]
		public virtual object GetRealObject(StreamingContext context)
		{
			return this.GetRealObjectHelper();
		}

		// Token: 0x06005497 RID: 21655 RVA: 0x0012BBE0 File Offset: 0x00129DE0
		[SecurityCritical]
		internal object GetRealObjectHelper()
		{
			if (!this.IsMarshaledObject())
			{
				return this;
			}
			if (this.IsObjRefLite())
			{
				int num = this.uri.IndexOf(RemotingConfiguration.ApplicationId);
				if (num > 0)
				{
					this.uri = this.uri.Substring(num - 1);
				}
			}
			bool fRefine = !(base.GetType() == typeof(ObjRef));
			object ret = RemotingServices.Unmarshal(this, fRefine);
			return this.GetCustomMarshaledCOMObject(ret);
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x0012BC54 File Offset: 0x00129E54
		[SecurityCritical]
		private object GetCustomMarshaledCOMObject(object ret)
		{
			DynamicTypeInfo dynamicTypeInfo = this.TypeInfo as DynamicTypeInfo;
			if (dynamicTypeInfo != null)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (this.IsFromThisProcess() && !this.IsFromThisAppDomain())
				{
					try
					{
						bool flag;
						intPtr = ((__ComObject)ret).GetIUnknown(out flag);
						if (intPtr != IntPtr.Zero && !flag)
						{
							string typeName = this.TypeInfo.TypeName;
							string name = null;
							string text = null;
							System.Runtime.Remoting.TypeInfo.ParseTypeAndAssembly(typeName, out name, out text);
							Assembly assembly = FormatterServices.LoadAssemblyFromStringNoThrow(text);
							if (assembly == null)
							{
								throw new RemotingException(Environment.GetResourceString("Serialization_AssemblyNotFound", new object[]
								{
									text
								}));
							}
							Type type = assembly.GetType(name, false, false);
							if (type != null && !type.IsVisible)
							{
								type = null;
							}
							object typedObjectForIUnknown = Marshal.GetTypedObjectForIUnknown(intPtr, type);
							if (typedObjectForIUnknown != null)
							{
								ret = typedObjectForIUnknown;
							}
						}
					}
					finally
					{
						if (intPtr != IntPtr.Zero)
						{
							Marshal.Release(intPtr);
						}
					}
				}
			}
			return ret;
		}

		// Token: 0x06005499 RID: 21657 RVA: 0x0012BD5C File Offset: 0x00129F5C
		public ObjRef()
		{
			this.objrefFlags = 0;
		}

		// Token: 0x0600549A RID: 21658 RVA: 0x0012BD6B File Offset: 0x00129F6B
		internal bool IsMarshaledObject()
		{
			return (this.objrefFlags & 1) == 1;
		}

		// Token: 0x0600549B RID: 21659 RVA: 0x0012BD78 File Offset: 0x00129F78
		internal void SetMarshaledObject()
		{
			this.objrefFlags |= 1;
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x0012BD88 File Offset: 0x00129F88
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal bool IsWellKnown()
		{
			return (this.objrefFlags & 2) == 2;
		}

		// Token: 0x0600549D RID: 21661 RVA: 0x0012BD95 File Offset: 0x00129F95
		internal void SetWellKnown()
		{
			this.objrefFlags |= 2;
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x0012BDA5 File Offset: 0x00129FA5
		internal bool HasProxyAttribute()
		{
			return (this.objrefFlags & 8) == 8;
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x0012BDB2 File Offset: 0x00129FB2
		internal void SetHasProxyAttribute()
		{
			this.objrefFlags |= 8;
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x0012BDC2 File Offset: 0x00129FC2
		internal bool IsObjRefLite()
		{
			return (this.objrefFlags & 4) == 4;
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x0012BDCF File Offset: 0x00129FCF
		internal void SetObjRefLite()
		{
			this.objrefFlags |= 4;
		}

		// Token: 0x060054A2 RID: 21666 RVA: 0x0012BDE0 File Offset: 0x00129FE0
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private CrossAppDomainData GetAppDomainChannelData()
		{
			for (int i = 0; i < this.ChannelInfo.ChannelData.Length; i++)
			{
				CrossAppDomainData crossAppDomainData = this.ChannelInfo.ChannelData[i] as CrossAppDomainData;
				if (crossAppDomainData != null)
				{
					return crossAppDomainData;
				}
			}
			return null;
		}

		// Token: 0x060054A3 RID: 21667 RVA: 0x0012BE20 File Offset: 0x0012A020
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool IsFromThisProcess()
		{
			if (this.IsWellKnown())
			{
				return false;
			}
			CrossAppDomainData appDomainChannelData = this.GetAppDomainChannelData();
			return appDomainChannelData != null && appDomainChannelData.IsFromThisProcess();
		}

		// Token: 0x060054A4 RID: 21668 RVA: 0x0012BE4C File Offset: 0x0012A04C
		[SecurityCritical]
		public bool IsFromThisAppDomain()
		{
			CrossAppDomainData appDomainChannelData = this.GetAppDomainChannelData();
			return appDomainChannelData != null && appDomainChannelData.IsFromThisAppDomain();
		}

		// Token: 0x060054A5 RID: 21669 RVA: 0x0012BE6C File Offset: 0x0012A06C
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal int GetServerDomainId()
		{
			if (!this.IsFromThisProcess())
			{
				return 0;
			}
			CrossAppDomainData appDomainChannelData = this.GetAppDomainChannelData();
			return appDomainChannelData.DomainID;
		}

		// Token: 0x060054A6 RID: 21670 RVA: 0x0012BE90 File Offset: 0x0012A090
		[SecurityCritical]
		internal IntPtr GetServerContext(out int domainId)
		{
			IntPtr result = IntPtr.Zero;
			domainId = 0;
			if (this.IsFromThisProcess())
			{
				CrossAppDomainData appDomainChannelData = this.GetAppDomainChannelData();
				domainId = appDomainChannelData.DomainID;
				if (AppDomain.IsDomainIdValid(appDomainChannelData.DomainID))
				{
					result = appDomainChannelData.ContextID;
				}
			}
			return result;
		}

		// Token: 0x060054A7 RID: 21671 RVA: 0x0012BED4 File Offset: 0x0012A0D4
		[SecurityCritical]
		internal void Init(object o, Identity idObj, RuntimeType requestedType)
		{
			this.uri = idObj.URI;
			MarshalByRefObject tporObject = idObj.TPOrObject;
			RuntimeType runtimeType;
			if (!RemotingServices.IsTransparentProxy(tporObject))
			{
				runtimeType = (RuntimeType)tporObject.GetType();
			}
			else
			{
				runtimeType = (RuntimeType)RemotingServices.GetRealProxy(tporObject).GetProxiedType();
			}
			RuntimeType runtimeType2 = (null == requestedType) ? runtimeType : requestedType;
			if (null != requestedType && !requestedType.IsAssignableFrom(runtimeType) && !typeof(IMessageSink).IsAssignableFrom(runtimeType))
			{
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_InvalidRequestedType"), requestedType.ToString()));
			}
			if (runtimeType.IsCOMObject)
			{
				DynamicTypeInfo dynamicTypeInfo = new DynamicTypeInfo(runtimeType2);
				this.TypeInfo = dynamicTypeInfo;
			}
			else
			{
				RemotingTypeCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(runtimeType2);
				this.TypeInfo = reflectionCachedData.TypeInfo;
			}
			if (!idObj.IsWellKnown())
			{
				this.EnvoyInfo = System.Runtime.Remoting.EnvoyInfo.CreateEnvoyInfo(idObj as ServerIdentity);
				IChannelInfo channelInfo = new ChannelInfo();
				if (o is AppDomain)
				{
					object[] channelData = channelInfo.ChannelData;
					int num = channelData.Length;
					object[] array = new object[num];
					Array.Copy(channelData, array, num);
					for (int i = 0; i < num; i++)
					{
						if (!(array[i] is CrossAppDomainData))
						{
							array[i] = null;
						}
					}
					channelInfo.ChannelData = array;
				}
				this.ChannelInfo = channelInfo;
				if (runtimeType.HasProxyAttribute)
				{
					this.SetHasProxyAttribute();
				}
			}
			else
			{
				this.SetWellKnown();
			}
			if (ObjRef.ShouldUseUrlObjRef())
			{
				if (this.IsWellKnown())
				{
					this.SetObjRefLite();
					return;
				}
				string text = ChannelServices.FindFirstHttpUrlForObject(this.URI);
				if (text != null)
				{
					this.URI = text;
					this.SetObjRefLite();
				}
			}
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x0012C069 File Offset: 0x0012A269
		internal static bool ShouldUseUrlObjRef()
		{
			return RemotingConfigHandler.UrlObjRefMode;
		}

		// Token: 0x060054A9 RID: 21673 RVA: 0x0012C070 File Offset: 0x0012A270
		[SecurityCritical]
		internal static bool IsWellFormed(ObjRef objectRef)
		{
			bool result = true;
			if (objectRef == null || objectRef.URI == null || (!objectRef.IsWellKnown() && !objectRef.IsObjRefLite() && !(objectRef.GetType() != ObjRef.orType) && objectRef.ChannelInfo == null))
			{
				result = false;
			}
			return result;
		}

		// Token: 0x040026AF RID: 9903
		internal const int FLG_MARSHALED_OBJECT = 1;

		// Token: 0x040026B0 RID: 9904
		internal const int FLG_WELLKNOWN_OBJREF = 2;

		// Token: 0x040026B1 RID: 9905
		internal const int FLG_LITE_OBJREF = 4;

		// Token: 0x040026B2 RID: 9906
		internal const int FLG_PROXY_ATTRIBUTE = 8;

		// Token: 0x040026B3 RID: 9907
		internal string uri;

		// Token: 0x040026B4 RID: 9908
		internal IRemotingTypeInfo typeInfo;

		// Token: 0x040026B5 RID: 9909
		internal IEnvoyInfo envoyInfo;

		// Token: 0x040026B6 RID: 9910
		internal IChannelInfo channelInfo;

		// Token: 0x040026B7 RID: 9911
		internal int objrefFlags;

		// Token: 0x040026B8 RID: 9912
		internal GCHandle srvIdentity;

		// Token: 0x040026B9 RID: 9913
		internal int domainID;

		// Token: 0x040026BA RID: 9914
		private static Type orType = typeof(ObjRef);
	}
}
