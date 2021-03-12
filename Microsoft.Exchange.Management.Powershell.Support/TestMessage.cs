using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriver;
using Microsoft.Exchange.Net.ExSmtpClient;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000012 RID: 18
	[Cmdlet("Test", "Message", SupportsShouldProcess = true)]
	public sealed class TestMessage : Task
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004777 File Offset: 0x00002977
		// (set) Token: 0x060000CD RID: 205 RVA: 0x0000478E File Offset: 0x0000298E
		[Parameter(Mandatory = false, ParameterSetName = "InboxRules")]
		[Parameter(Mandatory = true, ParameterSetName = "Arbitration")]
		[Parameter(Mandatory = false, ParameterSetName = "TransportRules")]
		public RecipientIdParameter SendReportTo
		{
			get
			{
				return (RecipientIdParameter)base.Fields["SendReportTo"];
			}
			set
			{
				base.Fields["SendReportTo"] = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000047A1 File Offset: 0x000029A1
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000047B8 File Offset: 0x000029B8
		[Parameter(Mandatory = false, ParameterSetName = "InboxRules")]
		[Parameter(Mandatory = false, ParameterSetName = "TransportRules")]
		public byte[] MessageFileData
		{
			get
			{
				return (byte[])base.Fields["MessageFileData"];
			}
			set
			{
				base.Fields["MessageFileData"] = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000047CB File Offset: 0x000029CB
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000047E2 File Offset: 0x000029E2
		[Parameter(Mandatory = false, ParameterSetName = "InboxRules")]
		[Parameter(Mandatory = true, ParameterSetName = "Arbitration")]
		[Parameter(Mandatory = false, ParameterSetName = "TransportRules")]
		public SmtpAddress Sender
		{
			get
			{
				return (SmtpAddress)base.Fields["Sender"];
			}
			set
			{
				base.Fields["Sender"] = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000047FA File Offset: 0x000029FA
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00004811 File Offset: 0x00002A11
		[Parameter(Mandatory = true, ParameterSetName = "TransportRules")]
		[Parameter(Mandatory = true, ParameterSetName = "Arbitration")]
		[Parameter(Mandatory = true, ParameterSetName = "InboxRules")]
		public ProxyAddressCollection Recipients
		{
			get
			{
				return (ProxyAddressCollection)base.Fields["Recipients"];
			}
			set
			{
				base.Fields["Recipients"] = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004824 File Offset: 0x00002A24
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x0000484A File Offset: 0x00002A4A
		[Parameter(Mandatory = false, ParameterSetName = "TransportRules")]
		[Parameter(Mandatory = false, ParameterSetName = "InboxRules")]
		public SwitchParameter DeliverMessage
		{
			get
			{
				return (SwitchParameter)(base.Fields["DeliverMessage"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DeliverMessage"] = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004862 File Offset: 0x00002A62
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00004879 File Offset: 0x00002A79
		[Parameter(Mandatory = false, ParameterSetName = "TransportRules")]
		[Parameter(Mandatory = false, ParameterSetName = "InboxRules")]
		public string Options
		{
			get
			{
				return (string)base.Fields["Options"];
			}
			set
			{
				base.Fields["Options"] = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000488C File Offset: 0x00002A8C
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000048B2 File Offset: 0x00002AB2
		[Parameter(Mandatory = true, ParameterSetName = "Arbitration")]
		public SwitchParameter Arbitration
		{
			get
			{
				return (SwitchParameter)(base.Fields["Arbitration"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Arbitration"] = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000048CA File Offset: 0x00002ACA
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000048F0 File Offset: 0x00002AF0
		[Parameter(Mandatory = true, ParameterSetName = "InboxRules")]
		public SwitchParameter InboxRules
		{
			get
			{
				return (SwitchParameter)(base.Fields["InboxRules"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["InboxRules"] = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004908 File Offset: 0x00002B08
		// (set) Token: 0x060000DD RID: 221 RVA: 0x0000492E File Offset: 0x00002B2E
		[Parameter(Mandatory = true, ParameterSetName = "TransportRules")]
		public SwitchParameter TransportRules
		{
			get
			{
				return (SwitchParameter)(base.Fields["TransportRules"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["TransportRules"] = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004946 File Offset: 0x00002B46
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestMessage;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004950 File Offset: 0x00002B50
		protected override void InternalValidate()
		{
			if (!this.WasSpecifiedByUser("MessageFileData") && !this.WasSpecifiedByUser("Sender"))
			{
				base.WriteError(new LocalizedException(Strings.MessageFileOrSenderMustBeSpecified), ErrorCategory.InvalidArgument, null);
				return;
			}
			if (this.WasSpecifiedByUser("MessageFileData") && this.MessageFileData == null)
			{
				base.WriteError(new LocalizedException(Strings.MessageFileDataSpecifiedAsNull), ErrorCategory.InvalidArgument, null);
				return;
			}
			this.GenerateMessage();
			this.EnsureSenderSpecified();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000049F4 File Offset: 0x00002BF4
		protected override void InternalProcessRecord()
		{
			int num = ServicePrincipalName.RegisterServiceClass("SmtpSvc");
			if (num == 0)
			{
				base.WriteVerbose(Strings.SpnRegistrationSucceeded);
			}
			else
			{
				this.WriteWarning(Strings.SpnRegistrationFailed(num));
			}
			List<string> recipients = new List<string>(this.Recipients.Count);
			foreach (ProxyAddress proxyAddress in this.Recipients)
			{
				recipients.Add(proxyAddress.AddressString);
			}
			using (ServerPickerManager serverPickerManager = new ServerPickerManager("Test-Message cmdlet", ServerRole.HubTransport, ExTraceGlobals.BridgeheadPickerTracer))
			{
				PickerServerList pickerServerList = serverPickerManager.GetPickerServerList();
				try
				{
					PickerServer pickerServer = null;
					bool flag = false;
					while (!flag)
					{
						PickerServer hub = pickerServerList.PickNextUsingRoundRobinPreferringLocal();
						if (hub == null)
						{
							this.WriteWarning(Strings.NoHubsAvailable);
							break;
						}
						if (pickerServer == null)
						{
							pickerServer = hub;
						}
						else if (hub == pickerServer)
						{
							this.WriteWarning(Strings.NoHubsAvailable);
							break;
						}
						base.WriteVerbose(Strings.TryingToSubmitTestmessage(hub.MachineName));
						int[] source = new int[]
						{
							25,
							2525
						};
						if (source.Any((int port) => this.TrySendMessage(recipients, hub, port)))
						{
							flag = true;
						}
					}
				}
				finally
				{
					pickerServerList.Release();
				}
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004BA0 File Offset: 0x00002DA0
		private bool TrySendMessage(IEnumerable<string> recipients, PickerServer hub, int portNumber)
		{
			bool result;
			using (SmtpClient smtpClient = new SmtpClient(hub.FQDN, portNumber, new TestMessage.SmtpClientDebugOutput(this)))
			{
				smtpClient.AuthCredentials(CredentialCache.DefaultNetworkCredentials);
				using (MemoryStream messageMemoryStream = this.GetMessageMemoryStream())
				{
					smtpClient.DataStream = messageMemoryStream;
					smtpClient.From = this.Sender.ToString();
					smtpClient.To = recipients.ToArray<string>();
					try
					{
						smtpClient.Submit();
					}
					catch (Exception ex)
					{
						base.WriteWarning(ex.Message);
						return false;
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004C5C File Offset: 0x00002E5C
		internal void GenerateMessage()
		{
			if (this.WasSpecifiedByUser("MessageFileData"))
			{
				string defaultDomain = this.GetDefaultDomain();
				string mimeError;
				if ((defaultDomain == null || !this.TryGenerateMessageFromMsgFileData(defaultDomain)) && !this.TryGenerateMessageFromEmlFileData(out mimeError))
				{
					base.WriteError(new LocalizedException(Strings.InvalidTestMessageFileData(mimeError)), ErrorCategory.InvalidArgument, null);
					return;
				}
			}
			else
			{
				this.GenerateDefaultMessage();
			}
			this.ApplyTestMessageHeaders();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004CB4 File Offset: 0x00002EB4
		internal MemoryStream GetMessageMemoryStream()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.message.MimeDocument.WriteTo(memoryStream);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004CE4 File Offset: 0x00002EE4
		private void GenerateDefaultMessage()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.ASCII);
				this.AddToFromHeader(streamWriter);
				this.AddDefaultBody(streamWriter);
				streamWriter.Flush();
				memoryStream.Flush();
				memoryStream.Position = 0L;
				this.message = EmailMessage.Create(memoryStream);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004D50 File Offset: 0x00002F50
		private void AddToFromHeader(StreamWriter writer)
		{
			writer.WriteLine("From: " + this.Sender.ToString());
			writer.Write("To: ");
			bool flag = true;
			foreach (ProxyAddress proxyAddress in this.Recipients)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					writer.Write(";");
				}
				writer.Write(proxyAddress.AddressString);
			}
			writer.WriteLine();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004DF4 File Offset: 0x00002FF4
		private void AddDefaultBody(StreamWriter writer)
		{
			writer.Write("Subject: ");
			writer.WriteLine(Strings.TestMessageDefaultSubject);
			writer.WriteLine();
			writer.WriteLine(Strings.TestMessageDefaultBody);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004E28 File Offset: 0x00003028
		private IRecipientSession GetRecipientSession()
		{
			ADObjectId adobjectId;
			OrganizationId organizationId = TaskHelper.ResolveCurrentUserOrganization(out adobjectId);
			if (organizationId == null)
			{
				organizationId = OrganizationId.ForestWideOrgId;
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerId(null, null), organizationId, organizationId, false);
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 474, "GetRecipientSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Support\\DiagnosticTasks\\TestMessage.cs");
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004E7C File Offset: 0x0000307C
		private IConfigurationSession GetConfigurationSession()
		{
			ADObjectId adobjectId;
			OrganizationId organizationId = TaskHelper.ResolveCurrentUserOrganization(out adobjectId);
			if (organizationId == null)
			{
				organizationId = OrganizationId.ForestWideOrgId;
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerId(null, null), organizationId, organizationId, false);
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 503, "GetConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Support\\DiagnosticTasks\\TestMessage.cs");
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004ED0 File Offset: 0x000030D0
		private string GetDefaultDomain()
		{
			if (base.MyInvocation.MyCommand == null)
			{
				return null;
			}
			string text = null;
			if (this.Recipients != null)
			{
				foreach (ProxyAddress proxyAddress in this.Recipients)
				{
					if (proxyAddress.Prefix == ProxyAddressPrefix.Smtp)
					{
						SmtpAddress smtpAddress = new SmtpAddress(proxyAddress.AddressString);
						text = smtpAddress.Domain;
						break;
					}
				}
			}
			if (text != null)
			{
				base.WriteVerbose(Strings.UsingDefaultDomainFromRecipient(text));
				return text;
			}
			IConfigurationSession configurationSession = this.GetConfigurationSession();
			AcceptedDomain defaultAcceptedDomain = configurationSession.GetDefaultAcceptedDomain();
			if (defaultAcceptedDomain != null)
			{
				text = defaultAcceptedDomain.DomainName.Domain;
				if (!string.IsNullOrEmpty(text))
				{
					base.WriteVerbose(Strings.UsingDefaultDomainFromAD(text));
					return text;
				}
			}
			base.WriteVerbose(Strings.UnableToDiscoverDefaultDomain);
			return null;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004FB4 File Offset: 0x000031B4
		private bool TryGenerateMessageFromMsgFileData(string defaultDomain)
		{
			bool result;
			using (Stream stream = new MemoryStream(this.MessageFileData))
			{
				using (MessageItem messageItem = MessageItem.CreateInMemory(StoreObjectSchema.ContentConversionProperties))
				{
					using (Stream stream2 = new MemoryStream())
					{
						try
						{
							InboundConversionOptions inboundConversionOptions = new InboundConversionOptions(defaultDomain);
							inboundConversionOptions.UserADSession = this.GetRecipientSession();
							OutboundConversionOptions outboundConversionOptions = new OutboundConversionOptions(defaultDomain);
							outboundConversionOptions.UserADSession = inboundConversionOptions.UserADSession;
							ItemConversion.ConvertMsgStorageToItem(stream, messageItem, inboundConversionOptions);
							if (this.WasSpecifiedByUser("Sender"))
							{
								SmtpAddress sender = this.Sender;
								Participant sender2 = new Participant(string.Empty, (string)this.Sender, "SMTP");
								messageItem.Sender = sender2;
							}
							ItemConversion.ConvertItemToSummaryTnef(messageItem, stream2, outboundConversionOptions);
							stream2.Position = 0L;
							this.message = EmailMessage.Create(stream2);
							result = true;
						}
						catch (CorruptDataException ex)
						{
							base.WriteVerbose(Strings.UnableToCreateFromMsg(ex.Message));
							result = false;
						}
						catch (ConversionFailedException ex2)
						{
							base.WriteVerbose(Strings.UnableToCreateFromMsg(ex2.Message));
							result = false;
						}
						catch (PropertyErrorException ex3)
						{
							base.WriteVerbose(Strings.UnableToCreateFromMsg(ex3.Message));
							result = false;
						}
						catch (StoragePermanentException ex4)
						{
							base.WriteVerbose(Strings.UnableToCreateFromMsg(ex4.Message));
							result = false;
						}
						catch (StorageTransientException ex5)
						{
							base.WriteVerbose(Strings.UnableToCreateFromMsg(ex5.Message));
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000051D0 File Offset: 0x000033D0
		private bool TryGenerateMessageFromEmlFileData(out string error)
		{
			error = string.Empty;
			MimeDocument mimeDocument = new MimeDocument();
			using (Stream stream = new MemoryStream(this.MessageFileData))
			{
				try
				{
					mimeDocument.ComplianceMode = MimeComplianceMode.Strict;
					mimeDocument.Load(stream, CachingMode.Copy);
					this.message = EmailMessage.Create(mimeDocument);
				}
				catch (ArgumentNullException)
				{
					return false;
				}
				catch (ArgumentException)
				{
					return false;
				}
				catch (InvalidOperationException)
				{
					return false;
				}
				catch (NotSupportedException)
				{
					return false;
				}
				catch (MimeException ex)
				{
					error = ex.Message;
					return false;
				}
			}
			if (mimeDocument.ComplianceStatus != MimeComplianceStatus.Compliant)
			{
				error = Strings.MimeDoesNotComplyWithStandards;
				return false;
			}
			return true;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000052A8 File Offset: 0x000034A8
		private void EnsureSenderSpecified()
		{
			if (!this.WasSpecifiedByUser("Sender"))
			{
				EmailRecipient from = this.message.From;
				if (from == null || from.SmtpAddress == null || !SmtpAddress.IsValidSmtpAddress(from.SmtpAddress))
				{
					base.WriteError(new LocalizedException(Strings.SenderNotSpecifiedAndNotPresentInMessage), ErrorCategory.InvalidArgument, null);
					return;
				}
				this.Sender = (SmtpAddress)from.SmtpAddress;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000530C File Offset: 0x0000350C
		private void ApplyTestMessageHeaders()
		{
			HeaderList headers = this.message.RootPart.Headers;
			string value = string.Empty;
			AsciiTextHeader newChild;
			if (this.WasSpecifiedByUser("Options"))
			{
				newChild = new AsciiTextHeader("X-MS-Exchange-Organization-Test-Message-Options", this.Options);
				headers.PrependChild(newChild);
			}
			if (this.WasSpecifiedByUser("SendReportTo"))
			{
				value = this.SendReportTo.ToString();
				newChild = new AsciiTextHeader("X-MS-Exchange-Organization-Test-Message-Send-Report-To", value);
				headers.PrependChild(newChild);
			}
			if (this.WasSpecifiedByUser("Arbitration"))
			{
				newChild = new AsciiTextHeader("X-MS-Exchange-Organization-Test-Message-Log-For", this.arbitrationLogHeaderValue);
			}
			else if (this.WasSpecifiedByUser("TransportRules"))
			{
				newChild = new AsciiTextHeader("X-MS-Exchange-Organization-Test-Message-Log-For", this.transportRulesLogHeaderValue);
			}
			else
			{
				newChild = new AsciiTextHeader("X-MS-Exchange-Organization-Test-Message-Log-For", this.inboxRulesLogHeaderValue);
			}
			headers.PrependChild(newChild);
			value = "Supress";
			if (this.WasSpecifiedByUser("DeliverMessage") || this.WasSpecifiedByUser("Arbitration"))
			{
				value = "Deliver";
			}
			newChild = new AsciiTextHeader("X-MS-Exchange-Organization-Test-Message", value);
			headers.PrependChild(newChild);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005418 File Offset: 0x00003618
		private bool WasSpecifiedByUser(string key)
		{
			return base.Fields.IsChanged(key) || base.Fields.IsModified(key);
		}

		// Token: 0x04000050 RID: 80
		private const string SendReportToKey = "SendReportTo";

		// Token: 0x04000051 RID: 81
		private const string MessageFileDataKey = "MessageFileData";

		// Token: 0x04000052 RID: 82
		private const string SenderKey = "Sender";

		// Token: 0x04000053 RID: 83
		private const string RecipientsKey = "Recipients";

		// Token: 0x04000054 RID: 84
		private const string DeliverMessageKey = "DeliverMessage";

		// Token: 0x04000055 RID: 85
		private const string OptionsKey = "Options";

		// Token: 0x04000056 RID: 86
		private const string ArbitrationKey = "Arbitration";

		// Token: 0x04000057 RID: 87
		private const string InboxRulesKey = "InboxRules";

		// Token: 0x04000058 RID: 88
		private const string TransportRulesKey = "TransportRules";

		// Token: 0x04000059 RID: 89
		private const string InboxRulesParameterSetName = "InboxRules";

		// Token: 0x0400005A RID: 90
		private const string ArbitrationParameterSetName = "Arbitration";

		// Token: 0x0400005B RID: 91
		private const string TransportRulesParameterSetName = "TransportRules";

		// Token: 0x0400005C RID: 92
		private readonly string inboxRulesLogHeaderValue = Enum.GetName(typeof(LogTypesEnum), LogTypesEnum.InboxRules);

		// Token: 0x0400005D RID: 93
		private readonly string arbitrationLogHeaderValue = Enum.GetName(typeof(LogTypesEnum), LogTypesEnum.Arbitration);

		// Token: 0x0400005E RID: 94
		private readonly string transportRulesLogHeaderValue = Enum.GetName(typeof(LogTypesEnum), LogTypesEnum.TransportRules);

		// Token: 0x0400005F RID: 95
		private EmailMessage message;

		// Token: 0x02000013 RID: 19
		private class SmtpClientDebugOutput : ISmtpClientDebugOutput
		{
			// Token: 0x060000EF RID: 239 RVA: 0x00005436 File Offset: 0x00003636
			public SmtpClientDebugOutput(TestMessage context)
			{
				this.context = context;
			}

			// Token: 0x060000F0 RID: 240 RVA: 0x00005445 File Offset: 0x00003645
			public void Output(Trace tracer, object context, string message, params object[] args)
			{
				if (message != null)
				{
					tracer.TraceDebug((long)((context != null) ? context.GetHashCode() : 0), message, args);
					this.context.WriteDebug(string.Format(message, args));
				}
			}

			// Token: 0x04000060 RID: 96
			private TestMessage context;
		}
	}
}
