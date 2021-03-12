using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000083 RID: 131
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class BackupKeyIsWrongTypeException : LocalizedException
	{
		// Token: 0x060006A9 RID: 1705 RVA: 0x000168A4 File Offset: 0x00014AA4
		public BackupKeyIsWrongTypeException(string keyName, string valueName) : base(Strings.BackupKeyIsWrongType(keyName, valueName))
		{
			this.keyName = keyName;
			this.valueName = valueName;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000168C1 File Offset: 0x00014AC1
		public BackupKeyIsWrongTypeException(string keyName, string valueName, Exception innerException) : base(Strings.BackupKeyIsWrongType(keyName, valueName), innerException)
		{
			this.keyName = keyName;
			this.valueName = valueName;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000168E0 File Offset: 0x00014AE0
		protected BackupKeyIsWrongTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.keyName = (string)info.GetValue("keyName", typeof(string));
			this.valueName = (string)info.GetValue("valueName", typeof(string));
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00016935 File Offset: 0x00014B35
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("keyName", this.keyName);
			info.AddValue("valueName", this.valueName);
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00016961 File Offset: 0x00014B61
		public string KeyName
		{
			get
			{
				return this.keyName;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x00016969 File Offset: 0x00014B69
		public string ValueName
		{
			get
			{
				return this.valueName;
			}
		}

		// Token: 0x040002FF RID: 767
		private readonly string keyName;

		// Token: 0x04000300 RID: 768
		private readonly string valueName;
	}
}
