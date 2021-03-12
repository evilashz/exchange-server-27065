using System;
using System.DirectoryServices;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000A5 RID: 165
	[Serializable]
	public class MailboxAcePresentationObject : AcePresentationObject
	{
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x0002D6E5 File Offset: 0x0002B8E5
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MailboxAcePresentationObject.schema;
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002D6EC File Offset: 0x0002B8EC
		public MailboxAcePresentationObject(ActiveDirectoryAccessRule ace, ADObjectId identity) : base(ace, identity)
		{
			this.AccessRights = new MailboxRights[]
			{
				(MailboxRights)ace.ActiveDirectoryRights
			};
			base.ResetChangeTracking();
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002D71E File Offset: 0x0002B91E
		public MailboxAcePresentationObject()
		{
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0002D726 File Offset: 0x0002B926
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x0002D738 File Offset: 0x0002B938
		[Parameter(Mandatory = true, ParameterSetName = "AccessRights")]
		[Parameter(Mandatory = false, ParameterSetName = "Instance")]
		public MailboxRights[] AccessRights
		{
			get
			{
				return (MailboxRights[])this[MailboxAcePresentationObjectSchema.AccessRights];
			}
			set
			{
				this[MailboxAcePresentationObjectSchema.AccessRights] = value;
			}
		}

		// Token: 0x04000244 RID: 580
		private static MailboxAcePresentationObjectSchema schema = ObjectSchema.GetInstance<MailboxAcePresentationObjectSchema>();
	}
}
