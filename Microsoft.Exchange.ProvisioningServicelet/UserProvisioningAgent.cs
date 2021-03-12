using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.ProvisioningServicelet;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class UserProvisioningAgent : ProvisioningAgent
	{
		// Token: 0x0600008D RID: 141 RVA: 0x000060D0 File Offset: 0x000042D0
		public UserProvisioningAgent(IProvisioningData data, ProvisioningAgentContext agentContext) : base(data, agentContext)
		{
			if (data.ProvisioningType != ProvisioningType.User)
			{
				throw new ArgumentException("data needs to be of UserProvisioningData type.");
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000060EE File Offset: 0x000042EE
		public override IMailboxData MailboxData
		{
			get
			{
				return this.mailboxData;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000060F6 File Offset: 0x000042F6
		protected virtual string[][] NewMailboxParameterMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000060F9 File Offset: 0x000042F9
		protected virtual string[][] GetMailboxParameterMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000060FC File Offset: 0x000042FC
		protected virtual string[][] SetMailboxParameterMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000060FF File Offset: 0x000042FF
		protected virtual string[][] SetMailboxParameterMapForPreexistingMailbox
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00006102 File Offset: 0x00004302
		protected virtual string[][] SetMailboxParameterMapForDCAdmin
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00006105 File Offset: 0x00004305
		protected virtual string[][] SetUserParameterMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00006108 File Offset: 0x00004308
		protected virtual string[][] ImportRecipientDataPropertyParameterMapForDCAdmin
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000610C File Offset: 0x0000430C
		protected override Error CreateRecipient()
		{
			ExTraceGlobals.WorkerTracer.TraceFunction(17748, (long)this.GetHashCode(), "CreateRecipient");
			Error result;
			try
			{
				result = this.Provision();
			}
			catch (Exception ex)
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(3840290109U);
				result = new Error(ProvisioningAgent.FilterTaskException(ex));
			}
			return result;
		}

		// Token: 0x06000097 RID: 151
		protected abstract Error Provision();

		// Token: 0x06000098 RID: 152 RVA: 0x0000616C File Offset: 0x0000436C
		protected override void IncrementPerfCounterForAttempt()
		{
			base.IncrementPerfCounterForAttempt();
			BulkUserProvisioningCounters.NumberOfMailboxesAttempted.Increment();
			BulkUserProvisioningCounters.RateOfMailboxesAttempted.Increment();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000618A File Offset: 0x0000438A
		protected override void IncrementPerfCounterForFailure()
		{
			base.IncrementPerfCounterForFailure();
			BulkUserProvisioningCounters.NumberOfMailboxesFailed.Increment();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000619D File Offset: 0x0000439D
		protected override void IncrementPerfCounterForCompletion()
		{
			base.IncrementPerfCounterForCompletion();
			BulkUserProvisioningCounters.NumberOfMailboxesCreated.Increment();
			BulkUserProvisioningCounters.RateOfMailboxesCreated.Increment();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000061BC File Offset: 0x000043BC
		protected Error SetMailbox(Mailbox mailbox)
		{
			this.UpdateProxyAddressesParameter(mailbox);
			ExTraceGlobals.WorkerTracer.TraceInformation(17776, (long)this.GetHashCode(), "invoke set-mailbox");
			PSCommand pscommand = new PSCommand().AddCommand("set-mailbox");
			pscommand.AddParameter("Identity", mailbox.Identity);
			if (base.PopulateParamsToPSCommand(pscommand, this.SetMailboxParameterMap, base.ProvisioningData.Parameters))
			{
				Error error;
				base.SafeRunPSCommand<Mailbox>(pscommand, base.AgentContext.Runspace, out error, null, null);
				if (error != null)
				{
					return error;
				}
			}
			pscommand = new PSCommand().AddCommand("set-mailbox");
			pscommand.AddParameter("Identity", mailbox.Identity);
			if (base.PopulateParamsToPSCommand(pscommand, this.SetMailboxParameterMapForDCAdmin, base.ProvisioningData.Parameters))
			{
				Error error;
				base.SafeRunPSCommand<Mailbox>(pscommand, base.AgentContext.DatacenterRunspace, out error, null, null);
				if (error != null)
				{
					return error;
				}
			}
			ExTraceGlobals.WorkerTracer.TraceInformation(17780, (long)this.GetHashCode(), "invoke Import-RecipientDataProperty");
			pscommand = new PSCommand().AddCommand("Import-RecipientDataProperty");
			pscommand.AddParameter("Identity", mailbox.Identity);
			if (base.PopulateParamsToPSCommand(pscommand, this.ImportRecipientDataPropertyParameterMapForDCAdmin, base.ProvisioningData.Parameters))
			{
				pscommand.AddParameter("SpokenName");
				pscommand.AddParameter("Confirm", false);
				Error error;
				base.SafeRunPSCommand<Mailbox>(pscommand, base.AgentContext.DatacenterRunspace, out error, null, null);
				if (error != null)
				{
					return error;
				}
			}
			ExTraceGlobals.WorkerTracer.TraceInformation(17780, (long)this.GetHashCode(), "invoke set-user");
			pscommand = new PSCommand().AddCommand("set-user");
			pscommand.AddParameter("Identity", mailbox.Identity);
			if (base.PopulateParamsToPSCommand(pscommand, this.SetUserParameterMap, base.ProvisioningData.Parameters))
			{
				Error error;
				base.SafeRunPSCommand<ADUser>(pscommand, base.AgentContext.Runspace, out error, null, null);
				return error;
			}
			return null;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000063C0 File Offset: 0x000045C0
		protected Error SetMailboxForPreexistingMailbox(Mailbox mailbox)
		{
			this.UpdateProxyAddressesParameter(mailbox);
			ExTraceGlobals.WorkerTracer.TraceInformation(17776, (long)this.GetHashCode(), "invoke set-mailbox [for Pre-existing mailbox]");
			PSCommand pscommand = new PSCommand().AddCommand("set-mailbox");
			pscommand.AddParameter("Identity", mailbox.Identity);
			if (base.PopulateParamsToPSCommand(pscommand, this.SetMailboxParameterMapForPreexistingMailbox, base.ProvisioningData.Parameters))
			{
				Error error;
				base.SafeRunPSCommand<Mailbox>(pscommand, base.AgentContext.Runspace, out error, null, null);
				if (error != null)
				{
					return error;
				}
			}
			return null;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006450 File Offset: 0x00004650
		protected Error NewMailbox(out Mailbox mailbox)
		{
			ExTraceGlobals.WorkerTracer.TraceInformation(17752, (long)this.GetHashCode(), "invoke new-mailbox");
			UserProvisioningData userProvisioningData = (UserProvisioningData)base.ProvisioningData;
			PSCommand pscommand = new PSCommand().AddCommand("new-mailbox");
			if (!base.PopulateParamsToPSCommand(pscommand, this.NewMailboxParameterMap, userProvisioningData.Parameters))
			{
				throw new InvalidOperationException("Developer error: No parameters were mapped for new-mailbox.");
			}
			if (!string.IsNullOrEmpty(userProvisioningData.Password))
			{
				pscommand.AddParameter("Password", userProvisioningData.Password.ConvertToSecureString());
			}
			Error error;
			mailbox = base.SafeRunPSCommand<Mailbox>(pscommand, base.AgentContext.Runspace, out error, null, null);
			if (error == null)
			{
				return null;
			}
			ExTraceGlobals.WorkerTracer.TraceError<Exception>(17760, (long)this.GetHashCode(), "new-mailbox failed with {0}", error.Exception);
			if (error.Exception is WLCDUnmanagedMemberExistsException && !userProvisioningData.IsBPOS && !userProvisioningData.EvictLiveId)
			{
				ExTraceGlobals.WorkerTracer.TraceInformation(17764, (long)this.GetHashCode(), "invoke new-mailbox -EvictLiveID");
				pscommand = new PSCommand().AddCommand("new-mailbox");
				pscommand.AddParameter("EvictLiveID");
				if (!string.IsNullOrEmpty(userProvisioningData.Password))
				{
					pscommand.AddParameter("Password", userProvisioningData.Password.ConvertToSecureString());
				}
				base.PopulateParamsToPSCommand(pscommand, this.NewMailboxParameterMap, base.ProvisioningData.Parameters);
				mailbox = base.SafeRunPSCommand<Mailbox>(pscommand, base.AgentContext.Runspace, out error, null, null);
			}
			return error;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000065DC File Offset: 0x000047DC
		protected Error GetMailbox(out Mailbox mailbox)
		{
			PSCommand command = new PSCommand().AddCommand("get-mailbox");
			if (!base.PopulateParamsToPSCommand(command, this.GetMailboxParameterMap, base.ProvisioningData.Parameters))
			{
				throw new InvalidOperationException("Developer error: No parameters were mapped for get-mailbox.");
			}
			Error error;
			mailbox = base.SafeRunPSCommand<Mailbox>(command, base.AgentContext.Runspace, out error, null, null);
			if (error != null)
			{
				return error;
			}
			return null;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006644 File Offset: 0x00004844
		protected Error ConvertMailUserToMailbox(out Mailbox mailbox)
		{
			UserProvisioningData userProvisioningData = (UserProvisioningData)base.ProvisioningData;
			mailbox = null;
			ExTraceGlobals.FaultInjectionTracer.TraceTest(3863358781U);
			PSCommand pscommand = new PSCommand().AddCommand("get-recipient");
			if (!base.PopulateParamsToPSCommand(pscommand, this.GetMailboxParameterMap, userProvisioningData.Parameters))
			{
				throw new InvalidOperationException("Developer error: No parameters were mapped for get-recipient.");
			}
			pscommand.AddParameter("RecipientType", RecipientType.MailUser.ToString());
			Error error;
			ReducedRecipient reducedRecipient = base.SafeRunPSCommand<ReducedRecipient>(pscommand, base.AgentContext.Runspace, out error, null, null);
			if (error != null)
			{
				return error;
			}
			if (reducedRecipient.ResourceType == null && !string.IsNullOrEmpty(userProvisioningData.Password))
			{
				pscommand = new PSCommand().AddCommand("Set-MailUser");
				pscommand.AddParameter("Identity", reducedRecipient.Identity);
				pscommand.AddParameter("Password", userProvisioningData.Password.ConvertToSecureString());
				base.PopulateParamsToPSCommand(pscommand, this.SetMailboxParameterMapForDCAdmin, userProvisioningData.Parameters);
				base.SafeRunPSCommand<MailUser>(pscommand, base.AgentContext.DatacenterRunspace, out error, null, null);
				if (error != null)
				{
					return error;
				}
			}
			pscommand = new PSCommand().AddCommand("enable-mailbox");
			pscommand.AddParameter("Identity", reducedRecipient.Identity);
			ExchangeResourceType valueOrDefault = reducedRecipient.ResourceType.GetValueOrDefault();
			ExchangeResourceType? exchangeResourceType;
			if (exchangeResourceType != null)
			{
				switch (valueOrDefault)
				{
				case ExchangeResourceType.Room:
					pscommand.AddParameter("Room");
					pscommand.AddParameter("AccountDisabled", true);
					break;
				case ExchangeResourceType.Equipment:
					pscommand.AddParameter("Equipment");
					pscommand.AddParameter("AccountDisabled", true);
					break;
				}
			}
			mailbox = base.SafeRunPSCommand<Mailbox>(pscommand, base.AgentContext.DatacenterRunspace, out error, null, null);
			if (error != null)
			{
				return error;
			}
			return null;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006820 File Offset: 0x00004A20
		protected override void UpdateProxyAddressesParameter(MailEnabledRecipient recipient)
		{
			base.UpdateProxyAddressesParameter(recipient);
			base.RemoveSmtpProxyAddressesWithExternalDomain();
		}

		// Token: 0x04000070 RID: 112
		protected IMailboxData mailboxData;
	}
}
