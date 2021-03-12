using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Reflection;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001AF RID: 431
	internal class UMMailboxPinCreator : MockObjectCreator
	{
		// Token: 0x06000F97 RID: 3991 RVA: 0x0002ECC0 File Offset: 0x0002CEC0
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"LockedOut",
				"PinExpired"
			};
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0002ECE8 File Offset: 0x0002CEE8
		protected override void FillProperties(Type type, PSObject psObject, object dummyObject, IList<string> properties)
		{
			foreach (PSMemberInfo psmemberInfo in psObject.Members)
			{
				if (properties.Contains(psmemberInfo.Name))
				{
					PropertyInfo property = dummyObject.GetType().GetProperty(psmemberInfo.Name);
					property.SetValue(dummyObject, MockObjectCreator.GetSingleProperty(psObject.Members[psmemberInfo.Name].Value, property.PropertyType), null);
				}
			}
		}
	}
}
