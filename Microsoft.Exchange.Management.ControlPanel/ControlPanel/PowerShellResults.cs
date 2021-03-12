using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006BB RID: 1723
	[DataContract]
	public class PowerShellResults
	{
		// Token: 0x06004966 RID: 18790 RVA: 0x000E0101 File Offset: 0x000DE301
		public PowerShellResults()
		{
			this.ErrorRecords = Array<ErrorRecord>.Empty;
			this.Warnings = Array<string>.Empty;
			this.Informations = Array<string>.Empty;
			this.Cmdlets = Array<string>.Empty;
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x000E0138 File Offset: 0x000DE338
		public PowerShellResults(PowerShellResults results)
		{
			this.ErrorRecords = results.ErrorRecords;
			this.Warnings = results.Warnings;
			this.Informations = results.Informations;
			this.Cmdlets = results.Cmdlets;
			this.TranslationIdentity = results.TranslationIdentity;
		}

		// Token: 0x170027E6 RID: 10214
		// (get) Token: 0x06004968 RID: 18792 RVA: 0x000E0187 File Offset: 0x000DE387
		public bool Succeeded
		{
			get
			{
				return this.ErrorRecords.IsNullOrEmpty();
			}
		}

		// Token: 0x170027E7 RID: 10215
		// (get) Token: 0x06004969 RID: 18793 RVA: 0x000E0194 File Offset: 0x000DE394
		public bool HasWarnings
		{
			get
			{
				return !this.Warnings.IsNullOrEmpty();
			}
		}

		// Token: 0x170027E8 RID: 10216
		// (get) Token: 0x0600496A RID: 18794 RVA: 0x000E01A4 File Offset: 0x000DE3A4
		public bool SucceededWithoutWarnings
		{
			get
			{
				return this.Succeeded && !this.HasWarnings;
			}
		}

		// Token: 0x170027E9 RID: 10217
		// (get) Token: 0x0600496B RID: 18795 RVA: 0x000E01B9 File Offset: 0x000DE3B9
		public bool Failed
		{
			get
			{
				return !this.Succeeded;
			}
		}

		// Token: 0x170027EA RID: 10218
		// (get) Token: 0x0600496C RID: 18796 RVA: 0x000E01C4 File Offset: 0x000DE3C4
		// (set) Token: 0x0600496D RID: 18797 RVA: 0x000E01CC File Offset: 0x000DE3CC
		[DataMember(EmitDefaultValue = false)]
		public JsonDictionary<object> OutputOnError { get; set; }

		// Token: 0x170027EB RID: 10219
		// (get) Token: 0x0600496E RID: 18798 RVA: 0x000E01D5 File Offset: 0x000DE3D5
		// (set) Token: 0x0600496F RID: 18799 RVA: 0x000E01DD File Offset: 0x000DE3DD
		[DataMember]
		public ErrorRecord[] ErrorRecords { get; set; }

		// Token: 0x170027EC RID: 10220
		// (get) Token: 0x06004970 RID: 18800 RVA: 0x000E01E6 File Offset: 0x000DE3E6
		// (set) Token: 0x06004971 RID: 18801 RVA: 0x000E01EE File Offset: 0x000DE3EE
		[DataMember]
		public string[] Warnings { get; set; }

		// Token: 0x170027ED RID: 10221
		// (get) Token: 0x06004972 RID: 18802 RVA: 0x000E01F7 File Offset: 0x000DE3F7
		// (set) Token: 0x06004973 RID: 18803 RVA: 0x000E01FF File Offset: 0x000DE3FF
		[DataMember]
		public string[] Informations { get; set; }

		// Token: 0x170027EE RID: 10222
		// (get) Token: 0x06004974 RID: 18804 RVA: 0x000E0208 File Offset: 0x000DE408
		// (set) Token: 0x06004975 RID: 18805 RVA: 0x000E0210 File Offset: 0x000DE410
		[DataMember]
		public string[] Cmdlets { get; set; }

		// Token: 0x170027EF RID: 10223
		// (get) Token: 0x06004976 RID: 18806 RVA: 0x000E0219 File Offset: 0x000DE419
		// (set) Token: 0x06004977 RID: 18807 RVA: 0x000E0221 File Offset: 0x000DE421
		[DataMember(EmitDefaultValue = false)]
		public ProgressRecord ProgressRecord { get; set; }

		// Token: 0x170027F0 RID: 10224
		// (get) Token: 0x06004978 RID: 18808 RVA: 0x000E022A File Offset: 0x000DE42A
		// (set) Token: 0x06004979 RID: 18809 RVA: 0x000E0232 File Offset: 0x000DE432
		[DataMember(EmitDefaultValue = false)]
		public string ProgressId { get; set; }

		// Token: 0x170027F1 RID: 10225
		// (get) Token: 0x0600497A RID: 18810 RVA: 0x000E023B File Offset: 0x000DE43B
		// (set) Token: 0x0600497B RID: 18811 RVA: 0x000E0243 File Offset: 0x000DE443
		[DataMember]
		public bool IsDDIEnabled { get; set; }

		// Token: 0x170027F2 RID: 10226
		// (get) Token: 0x0600497C RID: 18812 RVA: 0x000E0274 File Offset: 0x000DE474
		// (set) Token: 0x0600497D RID: 18813 RVA: 0x000E02D5 File Offset: 0x000DE4D5
		[DataMember(EmitDefaultValue = false)]
		public bool ShouldRetry
		{
			get
			{
				if (this.Cmdlets.All((string cmdlet) => cmdlet.StartsWith("Get-", StringComparison.OrdinalIgnoreCase)))
				{
					return this.ErrorRecords.Any((ErrorRecord error) => typeof(TransientException).IsInstanceOfType(error.Exception));
				}
				return false;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170027F3 RID: 10227
		// (get) Token: 0x0600497E RID: 18814 RVA: 0x000E02DC File Offset: 0x000DE4DC
		// (set) Token: 0x0600497F RID: 18815 RVA: 0x000E02E4 File Offset: 0x000DE4E4
		[DataMember(EmitDefaultValue = false)]
		public CmdExecuteInfo[] CmdletLogInfo { get; set; }

		// Token: 0x06004980 RID: 18816 RVA: 0x000E02F0 File Offset: 0x000DE4F0
		public PowerShellResults MergeErrors(PowerShellResults results)
		{
			this.ErrorRecords = ((this.ErrorRecords == null) ? results.ErrorRecords : this.ErrorRecords.Concat(results.ErrorRecords ?? new ErrorRecord[0]).ToArray<ErrorRecord>());
			if (this.Warnings == null)
			{
				this.Warnings = results.Warnings;
			}
			else if (results.Warnings != null)
			{
				List<string> list = this.Warnings.ToList<string>();
				foreach (string item in results.Warnings)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
				this.Warnings = list.ToArray();
			}
			this.Informations = ((this.Informations == null) ? results.Informations : this.Informations.Concat(results.Informations ?? new string[0]).ToArray<string>());
			this.Cmdlets = ((this.Cmdlets == null) ? results.Cmdlets : this.Cmdlets.Concat(results.Cmdlets ?? new string[0]).ToArray<string>());
			this.CmdletLogInfo = ((this.CmdletLogInfo == null) ? results.CmdletLogInfo : this.CmdletLogInfo.Concat(results.CmdletLogInfo ?? new CmdExecuteInfo[0]).ToArray<CmdExecuteInfo>());
			if (!string.IsNullOrEmpty(results.ProgressId))
			{
				this.ProgressId = results.ProgressId;
			}
			this.OutputOnError = ((this.OutputOnError == null) ? results.OutputOnError : this.OutputOnError.Merge(results.OutputOnError));
			if (results.ProgressRecord != null)
			{
				this.ProgressRecord = results.ProgressRecord;
			}
			this.TranslationIdentity = (this.TranslationIdentity ?? results.TranslationIdentity);
			return results;
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x000E049F File Offset: 0x000DE69F
		public PowerShellResults<O> MergeErrors<O>(PowerShellResults<O> results)
		{
			return (PowerShellResults<O>)this.MergeErrors(results);
		}

		// Token: 0x170027F4 RID: 10228
		// (get) Token: 0x06004982 RID: 18818 RVA: 0x000E04AD File Offset: 0x000DE6AD
		// (set) Token: 0x06004983 RID: 18819 RVA: 0x000E04B5 File Offset: 0x000DE6B5
		public Identity TranslationIdentity { get; set; }

		// Token: 0x06004984 RID: 18820 RVA: 0x000E04C0 File Offset: 0x000DE6C0
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			if (this.Failed && this.ShouldTranslate)
			{
				foreach (ErrorRecord errorRecord in this.ErrorRecords)
				{
					errorRecord.Translate(this.TranslationIdentity);
				}
			}
		}

		// Token: 0x170027F5 RID: 10229
		// (get) Token: 0x06004985 RID: 18821 RVA: 0x000E0502 File Offset: 0x000DE702
		protected virtual bool ShouldTranslate
		{
			get
			{
				return PowerShellMessageTranslator.ShouldTranslate;
			}
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x000E0509 File Offset: 0x000DE709
		public virtual void UseAsRbacScopeInCurrentHttpContext()
		{
		}
	}
}
