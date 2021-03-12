using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200062A RID: 1578
	public abstract class SetPopImapConfiguration<TDataObject> : SetSingletonSystemConfigurationObjectTask<TDataObject> where TDataObject : PopImapAdConfiguration, new()
	{
		// Token: 0x060037C2 RID: 14274 RVA: 0x000E73E8 File Offset: 0x000E55E8
		public SetPopImapConfiguration()
		{
			TDataObject tdataObject = Activator.CreateInstance<TDataObject>();
			this.protocolName = tdataObject.ProtocolName;
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x060037C3 RID: 14275 RVA: 0x000E7498 File Offset: 0x000E5698
		// (set) Token: 0x060037C4 RID: 14276 RVA: 0x000E74AF File Offset: 0x000E56AF
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x060037C5 RID: 14277 RVA: 0x000E74C2 File Offset: 0x000E56C2
		protected override ObjectId RootId
		{
			get
			{
				return PopImapAdConfiguration.GetRootId(this.ServerObject, this.protocolName);
			}
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x060037C6 RID: 14278 RVA: 0x000E74D8 File Offset: 0x000E56D8
		protected Server ServerObject
		{
			get
			{
				if (this.serverObject == null)
				{
					ServerIdParameter serverIdParameter = this.Server ?? ServerIdParameter.Parse(Environment.MachineName);
					this.serverObject = (Server)base.GetDataObject<Server>(serverIdParameter, base.DataSession as IConfigurationSession, null, new LocalizedString?(Strings.ErrorServerNotFound(serverIdParameter.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter.ToString())));
				}
				return this.serverObject;
			}
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000E7548 File Offset: 0x000E5748
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			TDataObject dataObject = this.DataObject;
			if (dataObject.ExchangeVersion.IsOlderThan(PopImapAdConfiguration.MinimumSupportedExchangeObjectVersion))
			{
				TDataObject dataObject2 = this.DataObject;
				string identity = dataObject2.Identity.ToString();
				TDataObject dataObject3 = this.DataObject;
				base.WriteError(new TaskException(Strings.ErrorSetOlderVirtualDirectory(identity, dataObject3.ExchangeVersion.ToString(), PopImapAdConfiguration.MinimumSupportedExchangeObjectVersion.ToString())), ErrorCategory.InvalidArgument, null);
			}
			this.ValidateSetServerRoleSpecificParameters();
			HashSet<int> hashSet = new HashSet<int>();
			TDataObject dataObject4 = this.DataObject;
			foreach (IPBinding ipbinding in dataObject4.UnencryptedOrTLSBindings)
			{
				hashSet.Add(ipbinding.Port);
			}
			HashSet<int> hashSet2 = new HashSet<int>();
			TDataObject dataObject5 = this.DataObject;
			foreach (IPBinding ipbinding2 in dataObject5.SSLBindings)
			{
				hashSet2.Add(ipbinding2.Port);
			}
			object[] customAttributes = base.GetType().GetCustomAttributes(typeof(CmdletAttribute), false);
			string noun = (customAttributes.Length > 0) ? ((CmdletAttribute)customAttributes[0]).NounName : string.Empty;
			TDataObject dataObject6 = this.DataObject;
			foreach (ProtocolConnectionSettings protocolConnectionSettings in dataObject6.InternalConnectionSettings)
			{
				if (((protocolConnectionSettings.EncryptionType == null || protocolConnectionSettings.EncryptionType == EncryptionType.TLS) && !hashSet.Contains(protocolConnectionSettings.Port)) || (protocolConnectionSettings.EncryptionType == EncryptionType.SSL && !hashSet2.Contains(protocolConnectionSettings.Port)))
				{
					string name = PopImapAdConfigurationSchema.InternalConnectionSettings.Name;
					TDataObject dataObject7 = this.DataObject;
					this.WriteWarning(Strings.PopImapSettingsPortMismatch(name, dataObject7.ProtocolName, noun));
					break;
				}
			}
			TDataObject dataObject8 = this.DataObject;
			foreach (ProtocolConnectionSettings protocolConnectionSettings2 in dataObject8.ExternalConnectionSettings)
			{
				if (((protocolConnectionSettings2.EncryptionType == null || protocolConnectionSettings2.EncryptionType == EncryptionType.TLS) && !hashSet.Contains(protocolConnectionSettings2.Port)) || (protocolConnectionSettings2.EncryptionType == EncryptionType.SSL && !hashSet2.Contains(protocolConnectionSettings2.Port)))
				{
					string name2 = PopImapAdConfigurationSchema.ExternalConnectionSettings.Name;
					TDataObject dataObject9 = this.DataObject;
					this.WriteWarning(Strings.PopImapSettingsPortMismatch(name2, dataObject9.ProtocolName, noun));
					break;
				}
			}
			if (this.DataObject.propertyBag.Changed)
			{
				TDataObject dataObject10 = this.DataObject;
				string protocol = dataObject10.ProtocolName;
				TDataObject dataObject11 = this.DataObject;
				this.WriteWarning(Strings.ChangesTakeEffectAfterRestartingPopImapService(protocol, dataObject11.ProtocolName, this.ServerObject.Name));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000E790C File Offset: 0x000E5B0C
		protected virtual void ValidateSetServerRoleSpecificParameters()
		{
			if ((this.ServerObject.IsClientAccessServer && this.ServerObject.IsCafeServer) || !this.ServerObject.IsE15OrLater)
			{
				return;
			}
			if (this.ServerObject.IsCafeServer)
			{
				foreach (string text in this.BrickRoleRequiredForFields)
				{
					if (base.UserSpecifiedParameters[text] != null)
					{
						this.WriteError(new ExInvalidArgumentForServerRoleException(text, Strings.InstallCafeRoleDescription), ErrorCategory.InvalidArgument, null, false);
					}
				}
				return;
			}
			if (this.ServerObject.IsClientAccessServer)
			{
				foreach (string text2 in this.CafeRoleRequiredForFields)
				{
					if (base.UserSpecifiedParameters[text2] != null)
					{
						this.WriteError(new ExInvalidArgumentForServerRoleException(text2, Strings.InstallMailboxRoleDescription), ErrorCategory.InvalidArgument, null, false);
					}
				}
			}
		}

		// Token: 0x040025B2 RID: 9650
		private readonly string protocolName;

		// Token: 0x040025B3 RID: 9651
		private readonly string[] BrickRoleRequiredForFields = new string[]
		{
			"ProxyTargetPort",
			"CalendarItemRetrievalOption",
			"EnableExactRFC822Size",
			"MessageRetrievalMimeFormat",
			"OwaServerUrl",
			"SuppressReadReceipt"
		};

		// Token: 0x040025B4 RID: 9652
		private readonly string[] CafeRoleRequiredForFields = new string[]
		{
			"Banner",
			"ExternalConnectionSettings",
			"InternalConnectionSettings",
			"MaxConnectionFromSingleIP",
			"PreAuthenticatedConnectionTimeout",
			"UnencryptedOrTLSBindings",
			"SSLBindings"
		};

		// Token: 0x040025B5 RID: 9653
		private Server serverObject;
	}
}
