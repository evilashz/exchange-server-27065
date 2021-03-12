using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000157 RID: 343
	internal sealed class HasBlockedImagesProperty : ComplexPropertyBase, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x0002E204 File Offset: 0x0002C404
		public HasBlockedImagesProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0002E20D File Offset: 0x0002C40D
		public static HasBlockedImagesProperty CreateCommand(CommandContext commandContext)
		{
			return new HasBlockedImagesProperty(commandContext);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0002E215 File Offset: 0x0002C415
		public void ToXml()
		{
			throw new InvalidOperationException("HasBlockedImagesProperty.ToXml should not be called.");
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0002E224 File Offset: 0x0002C424
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			if (EWSSettings.ItemHasBlockedImages != null)
			{
				serviceObject.PropertyBag[propertyInformation] = EWSSettings.ItemHasBlockedImages;
				return;
			}
			serviceObject.PropertyBag[propertyInformation] = false;
		}
	}
}
