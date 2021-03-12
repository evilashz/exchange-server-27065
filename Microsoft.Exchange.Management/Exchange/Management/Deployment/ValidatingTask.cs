using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000288 RID: 648
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class ValidatingTask : Task
	{
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x00064380 File Offset: 0x00062580
		// (set) Token: 0x060017B1 RID: 6065 RVA: 0x00064388 File Offset: 0x00062588
		internal ValidatingCondition[] ValidationTests
		{
			get
			{
				return this.validationTests;
			}
			set
			{
				this.validationTests = value;
			}
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00064394 File Offset: 0x00062594
		internal string[] GetTestDescriptions()
		{
			TaskLogger.LogEnter();
			string[] array = new string[this.ValidationTests.Length];
			for (int i = 0; i < this.ValidationTests.Length; i++)
			{
				array[i] = this.ValidationTests[i].Description.ToString();
			}
			TaskLogger.LogExit();
			return array;
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x000643EC File Offset: 0x000625EC
		protected sealed override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			bool flag = true;
			foreach (ValidatingCondition validatingCondition in this.ValidationTests)
			{
				ValidatingTaskResult validatingTaskResult = new ValidatingTaskResult();
				validatingTaskResult.ConditionDescription = validatingCondition.Description.ToString();
				if (flag)
				{
					bool flag2 = false;
					try
					{
						flag2 = validatingCondition.Validate();
					}
					catch (LocalizedException ex)
					{
						TaskLogger.LogError(new LocalizedException(Strings.ExceptionValidatingConditionFailed(ex.Message), ex));
						validatingTaskResult.FailureDetails = ex;
						flag2 = false;
					}
					validatingTaskResult.Result = (flag2 ? ValidatingTaskResult.ResultType.Passed : ValidatingTaskResult.ResultType.Failed);
					if (!flag2 && validatingCondition.AbortValidationIfFailed)
					{
						flag = false;
					}
				}
				base.WriteObject(validatingTaskResult);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000A06 RID: 2566
		private ValidatingCondition[] validationTests;
	}
}
