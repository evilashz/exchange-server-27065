using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;

namespace Microsoft.Exchange.Provisioning
{
	// Token: 0x020001F6 RID: 502
	public abstract class ProvisioningHandler
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x00035E75 File Offset: 0x00034075
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x00035E7D File Offset: 0x0003407D
		public string TaskName
		{
			get
			{
				return this.taskName;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("TaskName");
				}
				this.taskName = value;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x00035EA4 File Offset: 0x000340A4
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x00035E99 File Offset: 0x00034099
		public LogMessageDelegate LogMessage
		{
			get
			{
				LogMessageDelegate result;
				if ((result = this.logMessage) == null)
				{
					result = delegate(string message)
					{
					};
				}
				return result;
			}
			set
			{
				this.logMessage = value;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x00035ED8 File Offset: 0x000340D8
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x00035ECD File Offset: 0x000340CD
		public WriteErrorDelegate WriteError
		{
			get
			{
				WriteErrorDelegate result;
				if ((result = this.writeError) == null)
				{
					result = delegate(LocalizedException ex, ExchangeErrorCategory category)
					{
					};
				}
				return result;
			}
			set
			{
				this.writeError = value;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00035F0A File Offset: 0x0003410A
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x00035F01 File Offset: 0x00034101
		public UserScope UserScope
		{
			get
			{
				return this.userScope;
			}
			set
			{
				this.userScope = value;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00035F1B File Offset: 0x0003411B
		// (set) Token: 0x060011A1 RID: 4513 RVA: 0x00035F12 File Offset: 0x00034112
		public PropertyBag UserSpecifiedParameters
		{
			get
			{
				return this.userSpecifiedParameters;
			}
			set
			{
				this.userSpecifiedParameters = value;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x00035F23 File Offset: 0x00034123
		// (set) Token: 0x060011A4 RID: 4516 RVA: 0x00035F2B File Offset: 0x0003412B
		public ProvisioningCache ProvisioningCache
		{
			get
			{
				return this.provisioningCache;
			}
			internal set
			{
				this.provisioningCache = value;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x00035F34 File Offset: 0x00034134
		// (set) Token: 0x060011A6 RID: 4518 RVA: 0x00035F3C File Offset: 0x0003413C
		internal string AgentName
		{
			get
			{
				return this.agentName;
			}
			set
			{
				this.agentName = value;
			}
		}

		// Token: 0x060011A7 RID: 4519
		public abstract IConfigurable ProvisionDefaultProperties(IConfigurable readOnlyIConfigurable);

		// Token: 0x060011A8 RID: 4520
		public abstract bool UpdateAffectedIConfigurable(IConfigurable writeableIConfigurable);

		// Token: 0x060011A9 RID: 4521
		public abstract bool PreInternalProcessRecord(IConfigurable writeableIConfigurable);

		// Token: 0x060011AA RID: 4522
		public abstract ProvisioningValidationError[] Validate(IConfigurable readOnlyIConfigurable);

		// Token: 0x060011AB RID: 4523
		public abstract ProvisioningValidationError[] ValidateUserScope();

		// Token: 0x060011AC RID: 4524
		public abstract void OnComplete(bool succeeded, Exception e);

		// Token: 0x04000417 RID: 1047
		protected List<ProvisioningValidationError> validationErrorsList;

		// Token: 0x04000418 RID: 1048
		private string agentName;

		// Token: 0x04000419 RID: 1049
		private string taskName;

		// Token: 0x0400041A RID: 1050
		private UserScope userScope;

		// Token: 0x0400041B RID: 1051
		private PropertyBag userSpecifiedParameters;

		// Token: 0x0400041C RID: 1052
		private ProvisioningCache provisioningCache;

		// Token: 0x0400041D RID: 1053
		private LogMessageDelegate logMessage;

		// Token: 0x0400041E RID: 1054
		private WriteErrorDelegate writeError;
	}
}
