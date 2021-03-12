using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010F1 RID: 4337
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DuplicateTPDKeyIdException : LocalizedException
	{
		// Token: 0x0600B3A0 RID: 45984 RVA: 0x0029B7C3 File Offset: 0x002999C3
		public DuplicateTPDKeyIdException(string keyIdType, string keyId) : base(Strings.DuplicateTPDKeyId(keyIdType, keyId))
		{
			this.keyIdType = keyIdType;
			this.keyId = keyId;
		}

		// Token: 0x0600B3A1 RID: 45985 RVA: 0x0029B7E0 File Offset: 0x002999E0
		public DuplicateTPDKeyIdException(string keyIdType, string keyId, Exception innerException) : base(Strings.DuplicateTPDKeyId(keyIdType, keyId), innerException)
		{
			this.keyIdType = keyIdType;
			this.keyId = keyId;
		}

		// Token: 0x0600B3A2 RID: 45986 RVA: 0x0029B800 File Offset: 0x00299A00
		protected DuplicateTPDKeyIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.keyIdType = (string)info.GetValue("keyIdType", typeof(string));
			this.keyId = (string)info.GetValue("keyId", typeof(string));
		}

		// Token: 0x0600B3A3 RID: 45987 RVA: 0x0029B855 File Offset: 0x00299A55
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("keyIdType", this.keyIdType);
			info.AddValue("keyId", this.keyId);
		}

		// Token: 0x170038F9 RID: 14585
		// (get) Token: 0x0600B3A4 RID: 45988 RVA: 0x0029B881 File Offset: 0x00299A81
		public string KeyIdType
		{
			get
			{
				return this.keyIdType;
			}
		}

		// Token: 0x170038FA RID: 14586
		// (get) Token: 0x0600B3A5 RID: 45989 RVA: 0x0029B889 File Offset: 0x00299A89
		public string KeyId
		{
			get
			{
				return this.keyId;
			}
		}

		// Token: 0x0400625F RID: 25183
		private readonly string keyIdType;

		// Token: 0x04006260 RID: 25184
		private readonly string keyId;
	}
}
