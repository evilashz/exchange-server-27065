using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000012 RID: 18
	[OutputType(new Type[]
	{
		typeof(ConsumerMailbox)
	})]
	[Cmdlet("Get", "ConsumerMailbox")]
	public sealed class GetConsumerMailbox : GetObjectWithIdentityTaskBase<ConsumerMailboxIdParameter, ADUser>
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00006255 File Offset: 0x00004455
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000625D File Offset: 0x0000445D
		private new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00006266 File Offset: 0x00004466
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000626E File Offset: 0x0000446E
		[Parameter(Mandatory = false)]
		public SwitchParameter MservDataOnly { get; set; }

		// Token: 0x060000AF RID: 175 RVA: 0x00006278 File Offset: 0x00004478
		protected override IConfigDataProvider CreateSession()
		{
			IConfigDataProvider configDataProvider = ConsumerMailboxHelper.CreateConsumerOrganizationSession();
			if (this.MservDataOnly.ToBool())
			{
				((IAggregateSession)configDataProvider).MbxReadMode = MbxReadMode.NoMbxRead;
			}
			else
			{
				((IAggregateSession)configDataProvider).MbxReadMode = MbxReadMode.OnlyIfLocatorDataAvailable;
			}
			return configDataProvider;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000062B6 File Offset: 0x000044B6
		protected override bool IsKnownException(Exception exception)
		{
			return ConsumerMailboxHelper.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000062CC File Offset: 0x000044CC
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			ADUser dataObject2 = (ADUser)dataObject;
			return new ConsumerMailbox(dataObject2);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000062F0 File Offset: 0x000044F0
		protected override void WriteResult(IConfigurable dataObject)
		{
			if (dataObject != null)
			{
				IConfigurable dataObject2 = this.ConvertDataObjectToPresentationObject(dataObject);
				base.WriteResult(dataObject2);
			}
		}
	}
}
