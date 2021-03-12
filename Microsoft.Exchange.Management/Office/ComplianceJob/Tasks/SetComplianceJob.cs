using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Office.ComplianceJob.Tasks
{
	// Token: 0x02000143 RID: 323
	public abstract class SetComplianceJob<TDataObject> : SetTenantADTaskBase<ComplianceJobIdParameter, TDataObject, TDataObject> where TDataObject : ComplianceJob, new()
	{
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00035DF2 File Offset: 0x00033FF2
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x00035E09 File Offset: 0x00034009
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x00035E1C File Offset: 0x0003401C
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x00035E33 File Offset: 0x00034033
		[Parameter(Mandatory = false)]
		public string Description
		{
			get
			{
				return (string)base.Fields["Description"];
			}
			set
			{
				base.Fields["Description"] = value;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x00035E46 File Offset: 0x00034046
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x00035E5D File Offset: 0x0003405D
		[Parameter(Mandatory = false)]
		public string[] ExchangeBinding
		{
			get
			{
				return (string[])base.Fields["ExchangeBinding"];
			}
			set
			{
				base.Fields["ExchangeBinding"] = value;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00035E70 File Offset: 0x00034070
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x00035E87 File Offset: 0x00034087
		[Parameter(Mandatory = false)]
		public string[] PublicFolderBinding
		{
			get
			{
				return (string[])base.Fields["PublicFolderBinding"];
			}
			set
			{
				base.Fields["PublicFolderBinding"] = value;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00035E9A File Offset: 0x0003409A
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x00035EB1 File Offset: 0x000340B1
		[Parameter(Mandatory = false)]
		public string[] SharePointBinding
		{
			get
			{
				return (string[])base.Fields["SharePointBinding"];
			}
			set
			{
				base.Fields["SharePointBinding"] = value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x00035EC4 File Offset: 0x000340C4
		// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x00035EDB File Offset: 0x000340DB
		[Parameter(Mandatory = false)]
		public string[] AddExchangeBinding
		{
			get
			{
				return (string[])base.Fields["AddExchangeBinding"];
			}
			set
			{
				base.Fields["AddExchangeBinding"] = value;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00035EEE File Offset: 0x000340EE
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x00035F05 File Offset: 0x00034105
		[Parameter(Mandatory = false)]
		public string[] AddPublicFolderBinding
		{
			get
			{
				return (string[])base.Fields["AddPublicFolderBinding"];
			}
			set
			{
				base.Fields["AddPublicFolderBinding"] = value;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x00035F18 File Offset: 0x00034118
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x00035F2F File Offset: 0x0003412F
		[Parameter(Mandatory = false)]
		public string[] AddSharePointBinding
		{
			get
			{
				return (string[])base.Fields["AddSharePointBinding"];
			}
			set
			{
				base.Fields["AddSharePointBinding"] = value;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00035F42 File Offset: 0x00034142
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x00035F59 File Offset: 0x00034159
		[Parameter(Mandatory = false)]
		public string[] RemoveExchangeBinding
		{
			get
			{
				return (string[])base.Fields["RemoveExchangeBinding"];
			}
			set
			{
				base.Fields["RemoveExchangeBinding"] = value;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00035F6C File Offset: 0x0003416C
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x00035F83 File Offset: 0x00034183
		[Parameter(Mandatory = false)]
		public string[] RemovePublicFolderBinding
		{
			get
			{
				return (string[])base.Fields["RemovePublicFolderBinding"];
			}
			set
			{
				base.Fields["RemovePublicFolderBinding"] = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00035F96 File Offset: 0x00034196
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x00035FAD File Offset: 0x000341AD
		[Parameter(Mandatory = false)]
		public string[] RemoveSharePointBinding
		{
			get
			{
				return (string[])base.Fields["RemoveSharePointBinding"];
			}
			set
			{
				base.Fields["RemoveSharePointBinding"] = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x00035FC0 File Offset: 0x000341C0
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x00035FE1 File Offset: 0x000341E1
		[Parameter(Mandatory = false)]
		public bool AllPublicFolderBindings
		{
			get
			{
				return (bool)(base.Fields["AllPublicFolderBindings"] ?? false);
			}
			set
			{
				base.Fields["AllPublicFolderBindings"] = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x00035FF9 File Offset: 0x000341F9
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x0003601A File Offset: 0x0003421A
		[Parameter(Mandatory = false)]
		public bool AllExchangeBindings
		{
			get
			{
				return (bool)(base.Fields["AllExchangeBindings"] ?? false);
			}
			set
			{
				base.Fields["AllExchangeBindings"] = value;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x00036032 File Offset: 0x00034232
		// (set) Token: 0x06000BB3 RID: 2995 RVA: 0x00036053 File Offset: 0x00034253
		[Parameter(Mandatory = false)]
		public bool AllSharePointBindings
		{
			get
			{
				return (bool)(base.Fields["AllSharePointBindings"] ?? false);
			}
			set
			{
				base.Fields["AllSharePointBindings"] = value;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0003606B File Offset: 0x0003426B
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x00036091 File Offset: 0x00034291
		[Parameter]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000360A9 File Offset: 0x000342A9
		protected override IConfigDataProvider CreateSession()
		{
			if (base.ExchangeRunspaceConfig == null)
			{
				base.ThrowTerminatingError(new ComplianceJobTaskException(Strings.UnableToDetermineExecutingUser), ErrorCategory.InvalidOperation, null);
			}
			return new ComplianceJobProvider(base.ExchangeRunspaceConfig.OrganizationId);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000360DC File Offset: 0x000342DC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				TDataObject dataObject = this.DataObject;
				if (dataObject.IsChanged(ComplianceJobSchema.DisplayName))
				{
					ComplianceJobProvider complianceJobProvider = (ComplianceJobProvider)base.DataSession;
					TDataObject dataObject2 = this.DataObject;
					if (complianceJobProvider.FindJobsByName<ComplianceSearch>(dataObject2.Name) != null)
					{
						TDataObject dataObject3 = this.DataObject;
						base.WriteError(new ComplianceSearchNameIsNotUniqueException(dataObject3.Name), ErrorCategory.InvalidArgument, this.DataObject);
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00036170 File Offset: 0x00034370
		protected override IConfigurable PrepareDataObject()
		{
			ComplianceJob complianceJob = (ComplianceJob)base.PrepareDataObject();
			bool flag = false;
			string[] array = null;
			if (this.ProcessBindings(complianceJob, ComplianceBindingType.ExchangeBinding, out flag, out array))
			{
				base.Fields["ExchangeBinding"] = array;
				complianceJob.ExchangeBindings = array;
				complianceJob.AllExchangeBindings = flag;
			}
			if (this.ProcessBindings(complianceJob, ComplianceBindingType.PublicFolderBinding, out flag, out array))
			{
				base.Fields["PublicFolderBinding"] = array;
				complianceJob.PublicFolderBindings = array;
				complianceJob.AllPublicFolderBindings = flag;
			}
			if (this.ProcessBindings(complianceJob, ComplianceBindingType.SharePointBinding, out flag, out array))
			{
				base.Fields["SharePointBinding"] = array;
				complianceJob.SharePointBindings = array;
				complianceJob.AllSharePointBindings = flag;
			}
			if ((complianceJob.ExchangeBindings == null || complianceJob.ExchangeBindings.Count == 0) && !complianceJob.AllExchangeBindings && (complianceJob.PublicFolderBindings == null || complianceJob.PublicFolderBindings.Count == 0) && !complianceJob.AllPublicFolderBindings && (complianceJob.SharePointBindings == null || complianceJob.SharePointBindings.Count == 0) && !complianceJob.AllSharePointBindings)
			{
				base.WriteError(new ComplianceJobTaskException(Strings.NoBindingsSet), ErrorCategory.InvalidArgument, null);
			}
			if (base.Fields.IsModified("Description"))
			{
				complianceJob.Description = this.Description;
			}
			if (base.Fields.IsModified("Name"))
			{
				complianceJob.Name = this.Name;
			}
			complianceJob.LastModifiedTime = DateTime.UtcNow;
			return complianceJob;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x000362DC File Offset: 0x000344DC
		private bool ProcessBindings(ComplianceJob dataObject, ComplianceBindingType bindingType, out bool processedAllBindings, out string[] processedBindings)
		{
			processedAllBindings = false;
			processedBindings = null;
			string text;
			string text2;
			bool flag;
			string[] array;
			string[] array2;
			string[] array3;
			MultiValuedProperty<string> multiValuedProperty;
			switch (bindingType)
			{
			case ComplianceBindingType.ExchangeBinding:
				text = "ExchangeBinding";
				text2 = "AllExchangeBindings";
				flag = (bool)(base.Fields["AllExchangeBindings"] ?? false);
				array = (string[])base.Fields["ExchangeBinding"];
				array2 = (string[])base.Fields["AddExchangeBinding"];
				array3 = (string[])base.Fields["RemoveExchangeBinding"];
				multiValuedProperty = dataObject.ExchangeBindings;
				processedAllBindings = dataObject.AllExchangeBindings;
				break;
			case ComplianceBindingType.SharePointBinding:
				text = "SharePointBinding";
				text2 = "AllSharePointBindings";
				flag = (bool)(base.Fields["AllSharePointBindings"] ?? false);
				array = (string[])base.Fields["SharePointBinding"];
				array2 = (string[])base.Fields["AddSharePointBinding"];
				array3 = (string[])base.Fields["RemoveSharePointBinding"];
				multiValuedProperty = dataObject.SharePointBindings;
				processedAllBindings = dataObject.AllSharePointBindings;
				break;
			case ComplianceBindingType.PublicFolderBinding:
				text = "PublicFolderBinding";
				text2 = "AllPublicFolderBindings";
				flag = (bool)(base.Fields["AllPublicFolderBindings"] ?? false);
				array = (string[])base.Fields["PublicFolderBinding"];
				array2 = (string[])base.Fields["AddPublicFolderBinding"];
				array3 = (string[])base.Fields["RemovePublicFolderBinding"];
				multiValuedProperty = dataObject.PublicFolderBindings;
				processedAllBindings = dataObject.AllPublicFolderBindings;
				break;
			default:
				return false;
			}
			bool result = false;
			if (base.Fields.IsModified(text))
			{
				result = true;
			}
			else if (multiValuedProperty != null)
			{
				array = multiValuedProperty.ToArray();
			}
			if (array2 != null && array2.Length > 0)
			{
				if (array == null)
				{
					array = array2;
				}
				else
				{
					array = array.Union(array2).ToArray<string>();
				}
				result = true;
			}
			if (array3 != null && array3.Length > 0 && array != null)
			{
				array = array.Except(array3).ToArray<string>();
				result = true;
			}
			if (base.Fields.IsModified(text2))
			{
				processedAllBindings = flag;
				result = true;
			}
			if (flag)
			{
				if (array != null || multiValuedProperty != null)
				{
					this.WriteWarning(Strings.AllSourceMailboxesParameterOverride(text2, text));
				}
				processedBindings = null;
			}
			else
			{
				processedBindings = array;
			}
			return result;
		}

		// Token: 0x040005A0 RID: 1440
		private const string ParameterExchangeBinding = "ExchangeBinding";

		// Token: 0x040005A1 RID: 1441
		private const string ParameterPublicFolderBinding = "PublicFolderBinding";

		// Token: 0x040005A2 RID: 1442
		private const string ParameterSharePointBinding = "SharePointBinding";

		// Token: 0x040005A3 RID: 1443
		private const string ParameterAddExchangeBinding = "AddExchangeBinding";

		// Token: 0x040005A4 RID: 1444
		private const string ParameterAddPublicFolderBinding = "AddPublicFolderBinding";

		// Token: 0x040005A5 RID: 1445
		private const string ParameterAddSharePointBinding = "AddSharePointBinding";

		// Token: 0x040005A6 RID: 1446
		private const string ParameterRemoveExchangeBinding = "RemoveExchangeBinding";

		// Token: 0x040005A7 RID: 1447
		private const string ParameterRemovePublicFolderBinding = "RemovePublicFolderBinding";

		// Token: 0x040005A8 RID: 1448
		private const string ParameterRemoveSharePointBinding = "RemoveSharePointBinding";

		// Token: 0x040005A9 RID: 1449
		private const string ParameterAllPublicFolderBindings = "AllPublicFolderBindings";

		// Token: 0x040005AA RID: 1450
		private const string ParameterAllExchangeBindings = "AllExchangeBindings";

		// Token: 0x040005AB RID: 1451
		private const string ParameterAllSharePointBindings = "AllSharePointBindings";

		// Token: 0x040005AC RID: 1452
		private const string ParameterForce = "Force";

		// Token: 0x040005AD RID: 1453
		private const string ParameterDescription = "Description";

		// Token: 0x040005AE RID: 1454
		private const string ParameterName = "Name";
	}
}
