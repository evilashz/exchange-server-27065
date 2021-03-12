using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200022F RID: 559
	internal class XsoReadFlagProperty : XsoBooleanProperty, IBooleanProperty, IProperty
	{
		// Token: 0x060014ED RID: 5357 RVA: 0x00079CC2 File Offset: 0x00077EC2
		public XsoReadFlagProperty(StorePropertyDefinition propertyDef) : base(propertyDef)
		{
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00079CCB File Offset: 0x00077ECB
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			base.InternalCopyFromModified(srcProperty);
			if (Command.CurrentCommand != null)
			{
				Command.CurrentCommand.AddInteractiveCall();
			}
		}
	}
}
