using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000082 RID: 130
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class BackupKeyInaccessibleException : LocalizedException
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x0001682C File Offset: 0x00014A2C
		public BackupKeyInaccessibleException(string keyName) : base(Strings.BackupKeyInaccessible(keyName))
		{
			this.keyName = keyName;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00016841 File Offset: 0x00014A41
		public BackupKeyInaccessibleException(string keyName, Exception innerException) : base(Strings.BackupKeyInaccessible(keyName), innerException)
		{
			this.keyName = keyName;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00016857 File Offset: 0x00014A57
		protected BackupKeyInaccessibleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.keyName = (string)info.GetValue("keyName", typeof(string));
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00016881 File Offset: 0x00014A81
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("keyName", this.keyName);
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0001689C File Offset: 0x00014A9C
		public string KeyName
		{
			get
			{
				return this.keyName;
			}
		}

		// Token: 0x040002FE RID: 766
		private readonly string keyName;
	}
}
