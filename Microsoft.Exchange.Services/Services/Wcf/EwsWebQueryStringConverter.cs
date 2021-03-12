using System;
using System.ServiceModel.Dispatcher;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B82 RID: 2946
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EwsWebQueryStringConverter : QueryStringConverter
	{
		// Token: 0x060055DB RID: 21979 RVA: 0x00110AC7 File Offset: 0x0010ECC7
		public EwsWebQueryStringConverter(QueryStringConverter originalConverter)
		{
			if (originalConverter == null)
			{
				throw new ArgumentNullException("originalConverter");
			}
			this.originalConverter = originalConverter;
		}

		// Token: 0x060055DC RID: 21980 RVA: 0x00110AE4 File Offset: 0x0010ECE4
		public override object ConvertStringToValue(string parameter, Type parameterType)
		{
			if (parameterType == typeof(UserPhotoSize))
			{
				return this.ConvertStringToUserPhotoSize(parameter);
			}
			return this.originalConverter.ConvertStringToValue(parameter, parameterType);
		}

		// Token: 0x060055DD RID: 21981 RVA: 0x00110B14 File Offset: 0x0010ED14
		private UserPhotoSize ConvertStringToUserPhotoSize(string value)
		{
			UserPhotoSize result;
			if (!Enum.TryParse<UserPhotoSize>(value, true, out result))
			{
				throw FaultExceptionUtilities.CreateFault(new ServiceArgumentException(CoreResources.IDs.ErrorInvalidPhotoSize, CoreResources.ErrorInvalidPhotoSize), FaultParty.Sender);
			}
			return result;
		}

		// Token: 0x04002ED4 RID: 11988
		private readonly QueryStringConverter originalConverter;
	}
}
