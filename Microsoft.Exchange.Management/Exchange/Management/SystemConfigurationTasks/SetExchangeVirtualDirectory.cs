using System;
using System.Collections;
using System.DirectoryServices;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C42 RID: 3138
	public abstract class SetExchangeVirtualDirectory<T> : SetVirtualDirectory<T> where T : ExchangeVirtualDirectory, new()
	{
		// Token: 0x17002489 RID: 9353
		// (get) Token: 0x06007692 RID: 30354 RVA: 0x001E3DBA File Offset: 0x001E1FBA
		// (set) Token: 0x06007693 RID: 30355 RVA: 0x001E3DDB File Offset: 0x001E1FDB
		[Parameter(Mandatory = false)]
		public ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
		{
			get
			{
				return (ExtendedProtectionTokenCheckingMode)(base.Fields["ExtendedProtectionTokenChecking"] ?? ExtendedProtectionTokenCheckingMode.None);
			}
			set
			{
				base.Fields["ExtendedProtectionTokenChecking"] = value;
			}
		}

		// Token: 0x1700248A RID: 9354
		// (get) Token: 0x06007694 RID: 30356 RVA: 0x001E3DF3 File Offset: 0x001E1FF3
		// (set) Token: 0x06007695 RID: 30357 RVA: 0x001E3E0F File Offset: 0x001E200F
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
		{
			get
			{
				return ExchangeVirtualDirectory.ExtendedProtectionFlagsToMultiValuedProperty((ExtendedProtectionFlag)base.Fields["ExtendedProtectionFlags"]);
			}
			set
			{
				base.Fields["ExtendedProtectionFlags"] = (int)ExchangeVirtualDirectory.ExtendedProtectionMultiValuedPropertyToFlags(value);
			}
		}

		// Token: 0x1700248B RID: 9355
		// (get) Token: 0x06007696 RID: 30358 RVA: 0x001E3E2C File Offset: 0x001E202C
		// (set) Token: 0x06007697 RID: 30359 RVA: 0x001E3E43 File Offset: 0x001E2043
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtendedProtectionSPNList
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["ExtendedProtectionSPNList"];
			}
			set
			{
				base.Fields["ExtendedProtectionSPNList"] = value;
			}
		}

		// Token: 0x06007698 RID: 30360 RVA: 0x001E3E58 File Offset: 0x001E2058
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			T dataObject = this.DataObject;
			string hostName = IisUtility.GetHostName(dataObject.MetabasePath);
			try
			{
				if (!new IisVersionValidCondition(hostName).Verify())
				{
					Exception exception = new ArgumentException(Strings.ErrorIisVersionIsInvalid(hostName), "Server");
					ErrorCategory category = ErrorCategory.InvalidArgument;
					T dataObject2 = this.DataObject;
					base.WriteError(exception, category, dataObject2.Identity);
					return;
				}
			}
			catch (IOException innerException)
			{
				Exception exception2 = new ArgumentException(Strings.ErrorCannotDetermineIisVersion(hostName), "Server", innerException);
				ErrorCategory category2 = ErrorCategory.InvalidArgument;
				T dataObject3 = this.DataObject;
				base.WriteError(exception2, category2, dataObject3.Identity);
			}
			catch (InvalidOperationException innerException2)
			{
				Exception exception3 = new ArgumentException(Strings.ErrorCannotDetermineIisVersion(hostName), "Server", innerException2);
				ErrorCategory category3 = ErrorCategory.InvalidArgument;
				T dataObject4 = this.DataObject;
				base.WriteError(exception3, category3, dataObject4.Identity);
			}
			ExtendedProtection.Validate(this, this.DataObject);
			base.VerifyIsWithinScopes((IConfigurationSession)base.DataSession, this.DataObject, true, new DataAccessTask<T>.ADObjectOutOfScopeString(Strings.ErrorCannotSetVirtualDirectoryOutOfWriteScope));
			TaskLogger.LogExit();
		}

		// Token: 0x06007699 RID: 30361 RVA: 0x001E3F98 File Offset: 0x001E2198
		internal void CommitMetabaseValues(ExchangeVirtualDirectory dataObject, ArrayList MetabasePropertiesToChange)
		{
			if (MetabasePropertiesToChange != null)
			{
				string metabasePath = dataObject.MetabasePath;
				Task.TaskErrorLoggingReThrowDelegate writeError = new Task.TaskErrorLoggingReThrowDelegate(this.WriteError);
				T dataObject2 = this.DataObject;
				using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(metabasePath, writeError, dataObject2.Identity))
				{
					IisUtility.SetProperties(directoryEntry, MetabasePropertiesToChange);
					directoryEntry.CommitChanges();
					IisUtility.CommitMetabaseChanges((dataObject.Server == null) ? null : dataObject.Server.Name);
				}
			}
		}

		// Token: 0x0600769A RID: 30362 RVA: 0x001E401C File Offset: 0x001E221C
		protected override IConfigurable PrepareDataObject()
		{
			ExchangeVirtualDirectory exchangeVirtualDirectory = (ExchangeVirtualDirectory)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			if (base.Fields.Contains("ExtendedProtectionTokenChecking"))
			{
				exchangeVirtualDirectory.ExtendedProtectionTokenChecking = (ExtendedProtectionTokenCheckingMode)base.Fields["ExtendedProtectionTokenChecking"];
			}
			if (base.Fields.Contains("ExtendedProtectionSPNList"))
			{
				exchangeVirtualDirectory.ExtendedProtectionSPNList = (MultiValuedProperty<string>)base.Fields["ExtendedProtectionSPNList"];
			}
			if (base.Fields.Contains("ExtendedProtectionFlags"))
			{
				ExtendedProtectionFlag flags = (ExtendedProtectionFlag)base.Fields["ExtendedProtectionFlags"];
				exchangeVirtualDirectory.ExtendedProtectionFlags = ExchangeVirtualDirectory.ExtendedProtectionFlagsToMultiValuedProperty(flags);
			}
			return exchangeVirtualDirectory;
		}

		// Token: 0x0600769B RID: 30363 RVA: 0x001E40CE File Offset: 0x001E22CE
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			ExtendedProtection.CommitToMetabase(this.DataObject, this);
			TaskLogger.LogExit();
		}
	}
}
