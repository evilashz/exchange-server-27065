using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SnapIn;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000A4 RID: 164
	public class ExchangeDataObjectValidator : IDataObjectValidator
	{
		// Token: 0x0600053E RID: 1342 RVA: 0x000141A4 File Offset: 0x000123A4
		public ValidationError[] Validate(object dataObject)
		{
			IConfigurable configurable = dataObject as IConfigurable;
			if (configurable != null && PSConnectionInfoSingleton.GetInstance().Type != OrganizationType.Cloud)
			{
				return configurable.Validate();
			}
			return new ValidationError[0];
		}
	}
}
