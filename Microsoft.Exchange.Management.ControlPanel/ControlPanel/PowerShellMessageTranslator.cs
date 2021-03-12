using System;
using System.Collections.Generic;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Management.Tasks.UM;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006BD RID: 1725
	public class PowerShellMessageTranslator
	{
		// Token: 0x060049A7 RID: 18855 RVA: 0x000E071C File Offset: 0x000DE91C
		public PowerShellMessageTranslator()
		{
			this.InitializeTranslationTable();
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x000E0738 File Offset: 0x000DE938
		protected virtual void InitializeTranslationTable()
		{
			this.AddEntry(typeof(WLCDDomainException), new NewStringMechanism(Strings.InternalError));
			NewStringMechanism entry = new NewStringMechanism(Strings.ManagementObjectNotFoundExceptionWithDisplayNameTranslation, Strings.ManagementObjectNotFoundExceptionNoDisplayNameTranslation, false);
			this.AddEntry(typeof(ManagementObjectNotFoundException), entry);
			this.AddEntry(typeof(ADNoSuchObjectException), entry);
			NewStringMechanism entry2 = new NewStringMechanism(Strings.TransientExceptionTranslation);
			this.AddEntry(typeof(TransientException), entry2);
			this.AddEntry(typeof(ShouldContinueException), new NoOpReplaceMechanism());
			this.AddEntry(typeof(MapiObjectNotFoundException), entry2);
			this.AddEntry(typeof(ADInvalidPasswordException), new NewStringMechanism(Strings.InvalidPasswordExceptionTranslation, false));
			NewStringMechanism entry3 = new NewStringMechanism(Strings.ObjectAlreadyExistsExceptionTranslationWithDisplayName, Strings.ObjectAlreadyExistsExceptionTranslationNoDisplayName, false);
			this.AddEntry(typeof(ADObjectAlreadyExistsException), entry3);
			this.AddEntry(typeof(MapiObjectAlreadyExistsException), entry3);
			this.AddEntry(typeof(TrackingSearchException), new NewStringMechanism(Strings.TrackingSearchExceptionTranslation));
			this.AddEntry(typeof(RuleValidationException), new NoOpReplaceMechanism());
			this.AddEntry(typeof(ADScopeException), new NewStringMechanism(Strings.ADScopeExceptionTranslation));
			this.AddEntry(typeof(SecurityException), new NewStringMechanism(Strings.SecurityExceptionTranslation));
			this.AddEntry(typeof(OverBudgetException), new NewStringMechanism(Strings.OverBudgetExceptionTranslation));
			this.AddEntry(typeof(SelfMemberAlreadyExistsException), new NewStringMechanism(Strings.MemberAlreadyExistsExceptionTranslation, false));
			this.AddEntry(typeof(SelfMemberNotFoundException), new NewStringMechanism(Strings.MemberNotFoundExceptionTranslation, false));
			this.AddEntry(typeof(Exception), new NoOpReplaceMechanism());
			this.AddEntry(typeof(DefaultPinGenerationException), new NewStringMechanism(Strings.VoicemailInvalidPINTranslation));
			this.AddEntry(typeof(UMRpcException), new NewStringMechanism(Strings.TransientExceptionTranslation));
			this.AddEntry(typeof(ExtensionNotUniqueException), new NewStringMechanism(Strings.PhoneNumberAlreadyInUseTranslation));
			this.AddEntry(typeof(RpcUMServerNotFoundException), new NewStringMechanism(Strings.TransientExceptionTranslation));
			this.AddEntry(typeof(GlobalRoutingEntryNotFoundException), new NewStringMechanism(Strings.TransientExceptionTranslation));
			this.AddEntry(typeof(InvalidOperationForGetUMMailboxPinException), new NewStringMechanism(Strings.TransientExceptionTranslation));
			this.AddEntry(typeof(QuotaExceededException), new NewStringMechanism(Strings.QuotaExceededException));
			this.AddEntry(typeof(PropertyValidationException), new PropertyValidationExceptionMechanism(false));
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x000E09B6 File Offset: 0x000DEBB6
		public string Translate(Identity id, Exception ex, string originalMessage)
		{
			return this.GetEntry(ex.GetType()).Translate(id, ex, originalMessage);
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x000E09CC File Offset: 0x000DEBCC
		protected void AddEntry(Type type, ITranslationMechanism entry)
		{
			this.entries[type] = entry;
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x000E09DC File Offset: 0x000DEBDC
		private ITranslationMechanism GetEntry(Type type)
		{
			ITranslationMechanism result = null;
			while (null != type)
			{
				if (this.entries.ContainsKey(type))
				{
					result = this.entries[type];
					break;
				}
				type = type.BaseType;
			}
			return result;
		}

		// Token: 0x17002803 RID: 10243
		// (get) Token: 0x060049AC RID: 18860 RVA: 0x000E0A1C File Offset: 0x000DEC1C
		public static bool ShouldTranslate
		{
			get
			{
				RbacPrincipal current = RbacPrincipal.GetCurrent(false);
				if (current == null)
				{
					return true;
				}
				bool flag = OrganizationId.ForestWideOrgId == current.RbacConfiguration.OrganizationId;
				return !flag || !current.IsAdmin;
			}
		}

		// Token: 0x17002804 RID: 10244
		// (get) Token: 0x060049AD RID: 18861 RVA: 0x000E0A59 File Offset: 0x000DEC59
		public static PowerShellMessageTranslator Instance
		{
			get
			{
				return PowerShellMessageTranslator.instance;
			}
		}

		// Token: 0x04003170 RID: 12656
		private Dictionary<Type, ITranslationMechanism> entries = new Dictionary<Type, ITranslationMechanism>();

		// Token: 0x04003171 RID: 12657
		private static readonly PowerShellMessageTranslator instance = new PowerShellMessageTranslator();
	}
}
