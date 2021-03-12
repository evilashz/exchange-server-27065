using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005B9 RID: 1465
	[Serializable]
	internal abstract class RuntimeFieldInfo : FieldInfo, ISerializable
	{
		// Token: 0x060044EF RID: 17647 RVA: 0x000FCFCF File Offset: 0x000FB1CF
		protected RuntimeFieldInfo()
		{
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x000FCFD7 File Offset: 0x000FB1D7
		protected RuntimeFieldInfo(RuntimeType.RuntimeTypeCache reflectedTypeCache, RuntimeType declaringType, BindingFlags bindingFlags)
		{
			this.m_bindingFlags = bindingFlags;
			this.m_declaringType = declaringType;
			this.m_reflectedTypeCache = reflectedTypeCache;
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x060044F1 RID: 17649 RVA: 0x000FCFF4 File Offset: 0x000FB1F4
		internal RemotingFieldCachedData RemotingCache
		{
			get
			{
				RemotingFieldCachedData remotingFieldCachedData = this.m_cachedData;
				if (remotingFieldCachedData == null)
				{
					remotingFieldCachedData = new RemotingFieldCachedData(this);
					RemotingFieldCachedData remotingFieldCachedData2 = Interlocked.CompareExchange<RemotingFieldCachedData>(ref this.m_cachedData, remotingFieldCachedData, null);
					if (remotingFieldCachedData2 != null)
					{
						remotingFieldCachedData = remotingFieldCachedData2;
					}
				}
				return remotingFieldCachedData;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x060044F2 RID: 17650 RVA: 0x000FD026 File Offset: 0x000FB226
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x060044F3 RID: 17651 RVA: 0x000FD02E File Offset: 0x000FB22E
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x060044F4 RID: 17652 RVA: 0x000FD03B File Offset: 0x000FB23B
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return this.m_declaringType;
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x000FD043 File Offset: 0x000FB243
		internal RuntimeType GetRuntimeType()
		{
			return this.m_declaringType;
		}

		// Token: 0x060044F6 RID: 17654
		internal abstract RuntimeModule GetRuntimeModule();

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x060044F7 RID: 17655 RVA: 0x000FD04B File Offset: 0x000FB24B
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x060044F8 RID: 17656 RVA: 0x000FD04E File Offset: 0x000FB24E
		public override Type ReflectedType
		{
			get
			{
				if (!this.m_reflectedTypeCache.IsGlobal)
				{
					return this.ReflectedTypeInternal;
				}
				return null;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x060044F9 RID: 17657 RVA: 0x000FD065 File Offset: 0x000FB265
		public override Type DeclaringType
		{
			get
			{
				if (!this.m_reflectedTypeCache.IsGlobal)
				{
					return this.m_declaringType;
				}
				return null;
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x060044FA RID: 17658 RVA: 0x000FD07C File Offset: 0x000FB27C
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x000FD084 File Offset: 0x000FB284
		public override string ToString()
		{
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				return this.FieldType.ToString() + " " + this.Name;
			}
			return this.FieldType.FormatTypeName() + " " + this.Name;
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x000FD0C4 File Offset: 0x000FB2C4
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x000FD0DC File Offset: 0x000FB2DC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x000FD130 File Offset: 0x000FB330
		[SecuritySafeCritical]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x000FD182 File Offset: 0x000FB382
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x000FD18A File Offset: 0x000FB38A
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), MemberTypes.Field);
		}

		// Token: 0x04001BFD RID: 7165
		private BindingFlags m_bindingFlags;

		// Token: 0x04001BFE RID: 7166
		protected RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x04001BFF RID: 7167
		protected RuntimeType m_declaringType;

		// Token: 0x04001C00 RID: 7168
		private RemotingFieldCachedData m_cachedData;
	}
}
