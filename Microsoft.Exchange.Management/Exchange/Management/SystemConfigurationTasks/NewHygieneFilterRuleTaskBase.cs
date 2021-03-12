using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A4E RID: 2638
	public abstract class NewHygieneFilterRuleTaskBase : NewMultitenancyFixedNameSystemConfigurationObjectTask<TransportRule>
	{
		// Token: 0x06005E71 RID: 24177 RVA: 0x0018BCDB File Offset: 0x00189EDB
		protected NewHygieneFilterRuleTaskBase(string ruleCollectionName)
		{
			this.ruleCollectionName = ruleCollectionName;
			this.Priority = 0;
			this.Enabled = true;
			base.Fields.ResetChangeTracking();
		}

		// Token: 0x17001C6E RID: 7278
		// (get) Token: 0x06005E72 RID: 24178 RVA: 0x0018BD03 File Offset: 0x00189F03
		// (set) Token: 0x06005E73 RID: 24179 RVA: 0x0018BD1A File Offset: 0x00189F1A
		[Parameter(Mandatory = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x17001C6F RID: 7279
		// (get) Token: 0x06005E74 RID: 24180 RVA: 0x0018BD2D File Offset: 0x00189F2D
		// (set) Token: 0x06005E75 RID: 24181 RVA: 0x0018BD44 File Offset: 0x00189F44
		[Parameter(Mandatory = false)]
		public int Priority
		{
			get
			{
				return (int)base.Fields["Priority"];
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Strings.NegativePriority);
				}
				base.Fields["Priority"] = value;
			}
		}

		// Token: 0x17001C70 RID: 7280
		// (get) Token: 0x06005E76 RID: 24182 RVA: 0x0018BD70 File Offset: 0x00189F70
		// (set) Token: 0x06005E77 RID: 24183 RVA: 0x0018BD87 File Offset: 0x00189F87
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base.Fields["Enabled"];
			}
			set
			{
				base.Fields["Enabled"] = value;
			}
		}

		// Token: 0x17001C71 RID: 7281
		// (get) Token: 0x06005E78 RID: 24184 RVA: 0x0018BD9F File Offset: 0x00189F9F
		// (set) Token: 0x06005E79 RID: 24185 RVA: 0x0018BDB6 File Offset: 0x00189FB6
		[Parameter(Mandatory = false)]
		public string Comments
		{
			get
			{
				return (string)base.Fields["Comments"];
			}
			set
			{
				base.Fields["Comments"] = value;
			}
		}

		// Token: 0x17001C72 RID: 7282
		// (get) Token: 0x06005E7A RID: 24186 RVA: 0x0018BDC9 File Offset: 0x00189FC9
		// (set) Token: 0x06005E7B RID: 24187 RVA: 0x0018BDE0 File Offset: 0x00189FE0
		protected TransportRulePredicate[] Conditions
		{
			get
			{
				return (TransportRulePredicate[])base.Fields["Conditions"];
			}
			set
			{
				base.Fields["Conditions"] = value;
			}
		}

		// Token: 0x17001C73 RID: 7283
		// (get) Token: 0x06005E7C RID: 24188 RVA: 0x0018BDF3 File Offset: 0x00189FF3
		// (set) Token: 0x06005E7D RID: 24189 RVA: 0x0018BE0A File Offset: 0x0018A00A
		protected TransportRulePredicate[] Exceptions
		{
			get
			{
				return (TransportRulePredicate[])base.Fields["Exceptions"];
			}
			set
			{
				base.Fields["Exceptions"] = value;
			}
		}

		// Token: 0x06005E7E RID: 24190 RVA: 0x0018BE20 File Offset: 0x0018A020
		protected override void InternalValidate()
		{
			Exception exception;
			string target;
			if (!Utils.ValidateParametersForRole(base.Fields, out exception, out target))
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, target);
			}
			if (!Utils.ValidateRecipientIdParameters(base.Fields, base.TenantGlobalCatalogSession, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), out exception, out target))
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, target);
			}
			try
			{
				List<Type> list;
				List<Type> list2;
				TransportRulePredicate[] array;
				TransportRulePredicate[] array2;
				Utils.BuildConditionsAndExceptionsFromParameters(base.Fields, base.TenantGlobalCatalogSession, base.DataSession, false, out list, out list2, out array, out array2);
				if (array.Length > 0)
				{
					this.Conditions = array;
				}
				if (array2.Length > 0)
				{
					this.Exceptions = array2;
				}
			}
			catch (Exception exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, this.Name);
			}
			if (this.Conditions == null)
			{
				base.WriteError(new ArgumentException(Strings.ErrorCannotCreateRuleWithoutCondition), ErrorCategory.InvalidArgument, this.Name);
			}
		}

		// Token: 0x040034E9 RID: 13545
		protected readonly string ruleCollectionName;
	}
}
