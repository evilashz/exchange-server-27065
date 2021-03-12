using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000965 RID: 2405
	internal class ImportDlpPolicyImpl : CmdletImplementation
	{
		// Token: 0x060055EF RID: 21999 RVA: 0x00161580 File Offset: 0x0015F780
		public ImportDlpPolicyImpl(ImportDlpPolicyTemplate dataObject)
		{
			this.dataObject = dataObject;
		}

		// Token: 0x060055F0 RID: 22000 RVA: 0x001615B0 File Offset: 0x0015F7B0
		public override void Validate()
		{
			if (this.dataObject.FileData == null)
			{
				this.dataObject.WriteError(new ArgumentException(Strings.ImportDlpPolicyFileDataIsNull), ErrorCategory.InvalidArgument, "FileData");
			}
			try
			{
				this.templates = DlpUtils.LoadDlpPolicyTemplates(this.dataObject.FileData);
			}
			catch (Exception ex)
			{
				if (!this.IsKnownException(ex))
				{
					throw;
				}
				this.dataObject.WriteError(ex, ErrorCategory.InvalidOperation, null);
			}
			using (IEnumerator<ADComplianceProgram> enumerator = DlpUtils.GetOutOfBoxDlpTemplates(base.DataSession).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ADComplianceProgram dlpPolicyTemplate = enumerator.Current;
					if (this.templates.Any((DlpPolicyTemplateMetaData a) => a.Name == dlpPolicyTemplate.Name))
					{
						this.dataObject.WriteError(new ArgumentException(Strings.ErrorDlpPolicyTemplateAlreadyInstalled(dlpPolicyTemplate.Name)), ErrorCategory.InvalidArgument, "FileData");
					}
				}
			}
		}

		// Token: 0x060055F1 RID: 22001 RVA: 0x001616C0 File Offset: 0x0015F8C0
		public override void ProcessRecord()
		{
			try
			{
				DlpUtils.SaveOutOfBoxDlpTemplates(base.DataSession, this.templates);
			}
			catch (Exception ex)
			{
				if (!this.IsKnownException(ex))
				{
					throw;
				}
				this.dataObject.WriteError(ex, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x060055F2 RID: 22002 RVA: 0x00161728 File Offset: 0x0015F928
		private bool IsKnownException(Exception e)
		{
			return ImportDlpPolicyImpl.KnownExceptions.Any((Type exceptionType) => exceptionType.IsInstanceOfType(e));
		}

		// Token: 0x040031CA RID: 12746
		private ImportDlpPolicyTemplate dataObject;

		// Token: 0x040031CB RID: 12747
		private IEnumerable<DlpPolicyTemplateMetaData> templates;

		// Token: 0x040031CC RID: 12748
		private static readonly Type[] KnownExceptions = new Type[]
		{
			typeof(DirectoryNotFoundException),
			typeof(IOException),
			typeof(DlpPolicyParsingException)
		};
	}
}
