using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Threading;

namespace System.Runtime.Serialization
{
	// Token: 0x02000713 RID: 1811
	internal sealed class SerializationFieldInfo : FieldInfo
	{
		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x060050B9 RID: 20665 RVA: 0x0011B40B File Offset: 0x0011960B
		public override Module Module
		{
			get
			{
				return this.m_field.Module;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x060050BA RID: 20666 RVA: 0x0011B418 File Offset: 0x00119618
		public override int MetadataToken
		{
			get
			{
				return this.m_field.MetadataToken;
			}
		}

		// Token: 0x060050BB RID: 20667 RVA: 0x0011B425 File Offset: 0x00119625
		internal SerializationFieldInfo(RuntimeFieldInfo field, string namePrefix)
		{
			this.m_field = field;
			this.m_serializationName = namePrefix + "+" + this.m_field.Name;
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x060050BC RID: 20668 RVA: 0x0011B450 File Offset: 0x00119650
		public override string Name
		{
			get
			{
				return this.m_serializationName;
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x060050BD RID: 20669 RVA: 0x0011B458 File Offset: 0x00119658
		public override Type DeclaringType
		{
			get
			{
				return this.m_field.DeclaringType;
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x060050BE RID: 20670 RVA: 0x0011B465 File Offset: 0x00119665
		public override Type ReflectedType
		{
			get
			{
				return this.m_field.ReflectedType;
			}
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x0011B472 File Offset: 0x00119672
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_field.GetCustomAttributes(inherit);
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x0011B480 File Offset: 0x00119680
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_field.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060050C1 RID: 20673 RVA: 0x0011B48F File Offset: 0x0011968F
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_field.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x060050C2 RID: 20674 RVA: 0x0011B49E File Offset: 0x0011969E
		public override Type FieldType
		{
			get
			{
				return this.m_field.FieldType;
			}
		}

		// Token: 0x060050C3 RID: 20675 RVA: 0x0011B4AB File Offset: 0x001196AB
		public override object GetValue(object obj)
		{
			return this.m_field.GetValue(obj);
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x0011B4BC File Offset: 0x001196BC
		[SecurityCritical]
		internal object InternalGetValue(object obj)
		{
			RtFieldInfo rtFieldInfo = this.m_field as RtFieldInfo;
			if (rtFieldInfo != null)
			{
				rtFieldInfo.CheckConsistency(obj);
				return rtFieldInfo.UnsafeGetValue(obj);
			}
			return this.m_field.GetValue(obj);
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x0011B4F9 File Offset: 0x001196F9
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			this.m_field.SetValue(obj, value, invokeAttr, binder, culture);
		}

		// Token: 0x060050C6 RID: 20678 RVA: 0x0011B510 File Offset: 0x00119710
		[SecurityCritical]
		internal void InternalSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			RtFieldInfo rtFieldInfo = this.m_field as RtFieldInfo;
			if (rtFieldInfo != null)
			{
				rtFieldInfo.CheckConsistency(obj);
				rtFieldInfo.UnsafeSetValue(obj, value, invokeAttr, binder, culture);
				return;
			}
			this.m_field.SetValue(obj, value, invokeAttr, binder, culture);
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x060050C7 RID: 20679 RVA: 0x0011B559 File Offset: 0x00119759
		internal RuntimeFieldInfo FieldInfo
		{
			get
			{
				return this.m_field;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x060050C8 RID: 20680 RVA: 0x0011B561 File Offset: 0x00119761
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				return this.m_field.FieldHandle;
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x060050C9 RID: 20681 RVA: 0x0011B56E File Offset: 0x0011976E
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_field.Attributes;
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x060050CA RID: 20682 RVA: 0x0011B57C File Offset: 0x0011977C
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

		// Token: 0x0400238A RID: 9098
		internal const string FakeNameSeparatorString = "+";

		// Token: 0x0400238B RID: 9099
		private RuntimeFieldInfo m_field;

		// Token: 0x0400238C RID: 9100
		private string m_serializationName;

		// Token: 0x0400238D RID: 9101
		private RemotingFieldCachedData m_cachedData;
	}
}
