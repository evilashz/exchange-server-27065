using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000103 RID: 259
	internal class BatchReferenceErrorReporter : IReferenceErrorReporter
	{
		// Token: 0x060007C2 RID: 1986 RVA: 0x00021744 File Offset: 0x0001F944
		public void ReportError(Task.ErrorLoggerDelegate writeError)
		{
			if (this.errors.Count > 0)
			{
				List<ReferenceParameterException> list = new List<ReferenceParameterException>();
				foreach (KeyValuePair<string, List<ReferenceException>> keyValuePair in this.errors)
				{
					ReferenceParameterException item = new ReferenceParameterException(Strings.ErrorReferenceParameter(keyValuePair.Key), keyValuePair.Key, keyValuePair.Value.ToArray());
					list.Add(item);
				}
				MultiReferenceParameterException exception = new MultiReferenceParameterException(Strings.ErrorMultiReferenceParameter(string.Join(", ", this.errors.Keys.ToArray<string>()), string.Join(", ", this.referenceValues.ToArray<string>())), list.ToArray());
				writeError(exception, ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00021824 File Offset: 0x0001FA24
		void IReferenceErrorReporter.ValidateReference(string parameter, string rawValue, ValidateReferenceDelegate validateReferenceMethood)
		{
			try
			{
				validateReferenceMethood(new Task.ErrorLoggerDelegate(this.WriteError));
			}
			catch (ManagementObjectNotFoundException innerException)
			{
				ReferenceNotFoundException error = new ReferenceNotFoundException(rawValue, innerException);
				this.AddError(parameter, error);
			}
			catch (ManagementObjectAmbiguousException innerException2)
			{
				ReferenceAmbiguousException error2 = new ReferenceAmbiguousException(rawValue, innerException2);
				this.AddError(parameter, error2);
			}
			catch (BatchReferenceErrorReporter.ValidationException ex)
			{
				LocalizedException ex2 = ex.InnerException as LocalizedException;
				ReferenceInvalidException error3;
				if (ex2 != null)
				{
					error3 = new ReferenceInvalidException(rawValue, ex2);
				}
				else
				{
					error3 = new ReferenceInvalidException(rawValue, ex.InnerException);
				}
				this.AddError(parameter, error3);
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x000218D0 File Offset: 0x0001FAD0
		private void AddError(string parameter, ReferenceException error)
		{
			if (!this.errors.ContainsKey(parameter))
			{
				this.errors.Add(parameter, new List<ReferenceException>());
			}
			this.errors[parameter].Add(error);
			if (!this.referenceValues.Contains(error.ReferenceValue))
			{
				this.referenceValues.Add(error.ReferenceValue);
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00021933 File Offset: 0x0001FB33
		private void WriteError(Exception exception, ExchangeErrorCategory category, object target)
		{
			throw new BatchReferenceErrorReporter.ValidationException(exception);
		}

		// Token: 0x040003A3 RID: 931
		private Dictionary<string, List<ReferenceException>> errors = new Dictionary<string, List<ReferenceException>>();

		// Token: 0x040003A4 RID: 932
		private HashSet<string> referenceValues = new HashSet<string>();

		// Token: 0x02000104 RID: 260
		private class ValidationException : Exception
		{
			// Token: 0x060007C7 RID: 1991 RVA: 0x00021959 File Offset: 0x0001FB59
			public ValidationException(Exception exception) : base(null, exception)
			{
			}
		}
	}
}
