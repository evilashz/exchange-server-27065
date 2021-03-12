using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Util;
using System.Threading;

namespace System.Security.Policy
{
	// Token: 0x02000349 RID: 841
	[ComVisible(true)]
	[Serializable]
	public sealed class HashMembershipCondition : ISerializable, IDeserializationCallback, IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IReportMatchMembershipCondition
	{
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06002AC6 RID: 10950 RVA: 0x0009ECF8 File Offset: 0x0009CEF8
		private object InternalSyncObject
		{
			get
			{
				if (this.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref this.s_InternalSyncObject, value, null);
				}
				return this.s_InternalSyncObject;
			}
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x0009ED27 File Offset: 0x0009CF27
		internal HashMembershipCondition()
		{
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x0009ED30 File Offset: 0x0009CF30
		private HashMembershipCondition(SerializationInfo info, StreamingContext context)
		{
			this.m_value = (byte[])info.GetValue("HashValue", typeof(byte[]));
			string text = (string)info.GetValue("HashAlgorithm", typeof(string));
			if (text != null)
			{
				this.m_hashAlg = HashAlgorithm.Create(text);
				return;
			}
			this.m_hashAlg = new SHA1Managed();
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x0009ED9C File Offset: 0x0009CF9C
		public HashMembershipCondition(HashAlgorithm hashAlg, byte[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (hashAlg == null)
			{
				throw new ArgumentNullException("hashAlg");
			}
			this.m_value = new byte[value.Length];
			Array.Copy(value, this.m_value, value.Length);
			this.m_hashAlg = hashAlg;
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x0009EDEF File Offset: 0x0009CFEF
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("HashValue", this.HashValue);
			info.AddValue("HashAlgorithm", this.HashAlgorithm.ToString());
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x0009EE18 File Offset: 0x0009D018
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06002ACD RID: 10957 RVA: 0x0009EE31 File Offset: 0x0009D031
		// (set) Token: 0x06002ACC RID: 10956 RVA: 0x0009EE1A File Offset: 0x0009D01A
		public HashAlgorithm HashAlgorithm
		{
			get
			{
				if (this.m_hashAlg == null && this.m_element != null)
				{
					this.ParseHashAlgorithm();
				}
				return this.m_hashAlg;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("HashAlgorithm");
				}
				this.m_hashAlg = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06002ACF RID: 10959 RVA: 0x0009EE7C File Offset: 0x0009D07C
		// (set) Token: 0x06002ACE RID: 10958 RVA: 0x0009EE4F File Offset: 0x0009D04F
		public byte[] HashValue
		{
			get
			{
				if (this.m_value == null && this.m_element != null)
				{
					this.ParseHashValue();
				}
				if (this.m_value == null)
				{
					return null;
				}
				byte[] array = new byte[this.m_value.Length];
				Array.Copy(this.m_value, array, this.m_value.Length);
				return array;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_value = new byte[value.Length];
				Array.Copy(value, this.m_value, value.Length);
			}
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x0009EECC File Offset: 0x0009D0CC
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x0009EEE4 File Offset: 0x0009D0E4
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			if (evidence == null)
			{
				return false;
			}
			Hash hostEvidence = evidence.GetHostEvidence<Hash>();
			if (hostEvidence != null)
			{
				if (this.m_value == null && this.m_element != null)
				{
					this.ParseHashValue();
				}
				if (this.m_hashAlg == null && this.m_element != null)
				{
					this.ParseHashAlgorithm();
				}
				byte[] array = null;
				object internalSyncObject = this.InternalSyncObject;
				lock (internalSyncObject)
				{
					array = hostEvidence.GenerateHash(this.m_hashAlg);
				}
				if (array != null && HashMembershipCondition.CompareArrays(array, this.m_value))
				{
					usedEvidence = hostEvidence;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x0009EF84 File Offset: 0x0009D184
		public IMembershipCondition Copy()
		{
			if (this.m_value == null && this.m_element != null)
			{
				this.ParseHashValue();
			}
			if (this.m_hashAlg == null && this.m_element != null)
			{
				this.ParseHashAlgorithm();
			}
			return new HashMembershipCondition(this.m_hashAlg, this.m_value);
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x0009EFC3 File Offset: 0x0009D1C3
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x0009EFCC File Offset: 0x0009D1CC
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x0009EFD8 File Offset: 0x0009D1D8
		public SecurityElement ToXml(PolicyLevel level)
		{
			if (this.m_value == null && this.m_element != null)
			{
				this.ParseHashValue();
			}
			if (this.m_hashAlg == null && this.m_element != null)
			{
				this.ParseHashAlgorithm();
			}
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.HashMembershipCondition");
			securityElement.AddAttribute("version", "1");
			if (this.m_value != null)
			{
				securityElement.AddAttribute("HashValue", Hex.EncodeHexString(this.HashValue));
			}
			if (this.m_hashAlg != null)
			{
				securityElement.AddAttribute("HashAlgorithm", this.HashAlgorithm.GetType().FullName);
			}
			return securityElement;
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x0009F080 File Offset: 0x0009D280
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (!e.Tag.Equals("IMembershipCondition"))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
			}
			object internalSyncObject = this.InternalSyncObject;
			lock (internalSyncObject)
			{
				this.m_element = e;
				this.m_value = null;
				this.m_hashAlg = null;
			}
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x0009F100 File Offset: 0x0009D300
		public override bool Equals(object o)
		{
			HashMembershipCondition hashMembershipCondition = o as HashMembershipCondition;
			if (hashMembershipCondition != null)
			{
				if (this.m_hashAlg == null && this.m_element != null)
				{
					this.ParseHashAlgorithm();
				}
				if (hashMembershipCondition.m_hashAlg == null && hashMembershipCondition.m_element != null)
				{
					hashMembershipCondition.ParseHashAlgorithm();
				}
				if (this.m_hashAlg != null && hashMembershipCondition.m_hashAlg != null && this.m_hashAlg.GetType() == hashMembershipCondition.m_hashAlg.GetType())
				{
					if (this.m_value == null && this.m_element != null)
					{
						this.ParseHashValue();
					}
					if (hashMembershipCondition.m_value == null && hashMembershipCondition.m_element != null)
					{
						hashMembershipCondition.ParseHashValue();
					}
					if (this.m_value.Length != hashMembershipCondition.m_value.Length)
					{
						return false;
					}
					for (int i = 0; i < this.m_value.Length; i++)
					{
						if (this.m_value[i] != hashMembershipCondition.m_value[i])
						{
							return false;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x0009F1E4 File Offset: 0x0009D3E4
		public override int GetHashCode()
		{
			if (this.m_hashAlg == null && this.m_element != null)
			{
				this.ParseHashAlgorithm();
			}
			int num = (this.m_hashAlg != null) ? this.m_hashAlg.GetType().GetHashCode() : 0;
			if (this.m_value == null && this.m_element != null)
			{
				this.ParseHashValue();
			}
			return num ^ HashMembershipCondition.GetByteArrayHashCode(this.m_value);
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x0009F248 File Offset: 0x0009D448
		public override string ToString()
		{
			if (this.m_hashAlg == null)
			{
				this.ParseHashAlgorithm();
			}
			return Environment.GetResourceString("Hash_ToString", new object[]
			{
				this.m_hashAlg.GetType().AssemblyQualifiedName,
				Hex.EncodeHexString(this.HashValue)
			});
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x0009F294 File Offset: 0x0009D494
		private void ParseHashValue()
		{
			object internalSyncObject = this.InternalSyncObject;
			lock (internalSyncObject)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("HashValue");
					if (text == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLElement", new object[]
						{
							"HashValue",
							base.GetType().FullName
						}));
					}
					this.m_value = Hex.DecodeHexString(text);
					if (this.m_value != null && this.m_hashAlg != null)
					{
						this.m_element = null;
					}
				}
			}
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x0009F340 File Offset: 0x0009D540
		private void ParseHashAlgorithm()
		{
			object internalSyncObject = this.InternalSyncObject;
			lock (internalSyncObject)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("HashAlgorithm");
					if (text != null)
					{
						this.m_hashAlg = HashAlgorithm.Create(text);
					}
					else
					{
						this.m_hashAlg = new SHA1Managed();
					}
					if (this.m_value != null && this.m_hashAlg != null)
					{
						this.m_element = null;
					}
				}
			}
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x0009F3C8 File Offset: 0x0009D5C8
		private static bool CompareArrays(byte[] first, byte[] second)
		{
			if (first.Length != second.Length)
			{
				return false;
			}
			int num = first.Length;
			for (int i = 0; i < num; i++)
			{
				if (first[i] != second[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x0009F3FC File Offset: 0x0009D5FC
		private static int GetByteArrayHashCode(byte[] baData)
		{
			if (baData == null)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < baData.Length; i++)
			{
				num = (num << 8 ^ (int)baData[i] ^ num >> 24);
			}
			return num;
		}

		// Token: 0x040010F8 RID: 4344
		private byte[] m_value;

		// Token: 0x040010F9 RID: 4345
		private HashAlgorithm m_hashAlg;

		// Token: 0x040010FA RID: 4346
		private SecurityElement m_element;

		// Token: 0x040010FB RID: 4347
		private object s_InternalSyncObject;

		// Token: 0x040010FC RID: 4348
		private const string s_tagHashValue = "HashValue";

		// Token: 0x040010FD RID: 4349
		private const string s_tagHashAlgorithm = "HashAlgorithm";
	}
}
