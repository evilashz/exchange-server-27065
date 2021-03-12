using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FA9 RID: 4009
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ManageIsapiExtensionFoundMoreThanOneExtensionException : LocalizedException
	{
		// Token: 0x0600AD16 RID: 44310 RVA: 0x00290F79 File Offset: 0x0028F179
		public ManageIsapiExtensionFoundMoreThanOneExtensionException(string groupId, string extensionBinary, string matches) : base(Strings.ManageIsapiExtensionFoundMoreThanOneExtensionException(groupId, extensionBinary, matches))
		{
			this.groupId = groupId;
			this.extensionBinary = extensionBinary;
			this.matches = matches;
		}

		// Token: 0x0600AD17 RID: 44311 RVA: 0x00290F9E File Offset: 0x0028F19E
		public ManageIsapiExtensionFoundMoreThanOneExtensionException(string groupId, string extensionBinary, string matches, Exception innerException) : base(Strings.ManageIsapiExtensionFoundMoreThanOneExtensionException(groupId, extensionBinary, matches), innerException)
		{
			this.groupId = groupId;
			this.extensionBinary = extensionBinary;
			this.matches = matches;
		}

		// Token: 0x0600AD18 RID: 44312 RVA: 0x00290FC8 File Offset: 0x0028F1C8
		protected ManageIsapiExtensionFoundMoreThanOneExtensionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupId = (string)info.GetValue("groupId", typeof(string));
			this.extensionBinary = (string)info.GetValue("extensionBinary", typeof(string));
			this.matches = (string)info.GetValue("matches", typeof(string));
		}

		// Token: 0x0600AD19 RID: 44313 RVA: 0x0029103D File Offset: 0x0028F23D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupId", this.groupId);
			info.AddValue("extensionBinary", this.extensionBinary);
			info.AddValue("matches", this.matches);
		}

		// Token: 0x1700378F RID: 14223
		// (get) Token: 0x0600AD1A RID: 44314 RVA: 0x0029107A File Offset: 0x0028F27A
		public string GroupId
		{
			get
			{
				return this.groupId;
			}
		}

		// Token: 0x17003790 RID: 14224
		// (get) Token: 0x0600AD1B RID: 44315 RVA: 0x00291082 File Offset: 0x0028F282
		public string ExtensionBinary
		{
			get
			{
				return this.extensionBinary;
			}
		}

		// Token: 0x17003791 RID: 14225
		// (get) Token: 0x0600AD1C RID: 44316 RVA: 0x0029108A File Offset: 0x0028F28A
		public string Matches
		{
			get
			{
				return this.matches;
			}
		}

		// Token: 0x040060F5 RID: 24821
		private readonly string groupId;

		// Token: 0x040060F6 RID: 24822
		private readonly string extensionBinary;

		// Token: 0x040060F7 RID: 24823
		private readonly string matches;
	}
}
