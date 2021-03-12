using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DEC RID: 3564
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotSaveContentSetting : LocalizedException
	{
		// Token: 0x0600A490 RID: 42128 RVA: 0x0028479E File Offset: 0x0028299E
		public CouldNotSaveContentSetting(string setting) : base(Strings.CouldNotSaveContentSetting(setting))
		{
			this.setting = setting;
		}

		// Token: 0x0600A491 RID: 42129 RVA: 0x002847B3 File Offset: 0x002829B3
		public CouldNotSaveContentSetting(string setting, Exception innerException) : base(Strings.CouldNotSaveContentSetting(setting), innerException)
		{
			this.setting = setting;
		}

		// Token: 0x0600A492 RID: 42130 RVA: 0x002847C9 File Offset: 0x002829C9
		protected CouldNotSaveContentSetting(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.setting = (string)info.GetValue("setting", typeof(string));
		}

		// Token: 0x0600A493 RID: 42131 RVA: 0x002847F3 File Offset: 0x002829F3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("setting", this.setting);
		}

		// Token: 0x170035FD RID: 13821
		// (get) Token: 0x0600A494 RID: 42132 RVA: 0x0028480E File Offset: 0x00282A0E
		public string Setting
		{
			get
			{
				return this.setting;
			}
		}

		// Token: 0x04005F63 RID: 24419
		private readonly string setting;
	}
}
