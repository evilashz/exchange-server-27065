using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting.Data
{
	// Token: 0x020003FF RID: 1023
	internal class ConversionProcessor : IDataProcessor
	{
		// Token: 0x06002410 RID: 9232 RVA: 0x000900E8 File Offset: 0x0008E2E8
		private ConversionProcessor(Type outputType, object parentInstance, int? startIndex)
		{
			this.targetOutputType = outputType;
			this.parentInstance = parentInstance;
			this.pagingIndex = startIndex;
			this.converters = Schema.Utilities.GetProperties<DalConversion>(this.targetOutputType);
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x00090124 File Offset: 0x0008E324
		internal static ConversionProcessor Create<TTargetConversionType>(object parentInstance)
		{
			if (parentInstance == null)
			{
				throw new ArgumentNullException("parentInstance");
			}
			return new ConversionProcessor(typeof(TTargetConversionType), parentInstance, null);
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x00090158 File Offset: 0x0008E358
		internal static ConversionProcessor CreatePageable<TTargetConversionType>(object parentInstance, int startIndex)
		{
			if (parentInstance == null)
			{
				throw new ArgumentNullException("parentInstance");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			return new ConversionProcessor(typeof(TTargetConversionType), parentInstance, new int?(startIndex));
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x00090190 File Offset: 0x0008E390
		public object Process(object input)
		{
			object obj = null;
			if (input != null)
			{
				obj = Activator.CreateInstance(this.targetOutputType);
				foreach (Tuple<PropertyInfo, DalConversion> tuple in this.converters)
				{
					DalConversion item = tuple.Item2;
					PropertyInfo item2 = tuple.Item1;
					try
					{
						item.SetOutput(obj, item2, input, this.parentInstance);
					}
					catch (NullReferenceException)
					{
						string arg = (item2 != null) ? item2.Name : "Unknown";
						string text = string.Format("Null dalObj {0} reportObj {1}", item.DalPropertyName, arg);
						ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_FfoReportingTaskFailure, new string[]
						{
							text
						});
					}
				}
				IPageableObject pageableObject = obj as IPageableObject;
				if (pageableObject != null && this.pagingIndex != null)
				{
					pageableObject.Index = this.pagingIndex.Value;
					this.pagingIndex++;
				}
			}
			return obj;
		}

		// Token: 0x04001CCC RID: 7372
		private readonly IList<Tuple<PropertyInfo, DalConversion>> converters;

		// Token: 0x04001CCD RID: 7373
		private readonly object parentInstance;

		// Token: 0x04001CCE RID: 7374
		private readonly Type targetOutputType;

		// Token: 0x04001CCF RID: 7375
		private int? pagingIndex = new int?(0);
	}
}
