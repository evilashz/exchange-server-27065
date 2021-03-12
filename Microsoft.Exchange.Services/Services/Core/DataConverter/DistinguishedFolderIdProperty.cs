using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000146 RID: 326
	internal sealed class DistinguishedFolderIdProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x0002BED0 File Offset: 0x0002A0D0
		public DistinguishedFolderIdProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0002BEDC File Offset: 0x0002A0DC
		private void ToServiceObject(StoreId storeId, StoreSession session, ServiceObject serviceObject)
		{
			if (EWSSettings.DistinguishedFolderIdNameDictionary == null)
			{
				EWSSettings.DistinguishedFolderIdNameDictionary = new DistinguishedFolderIdNameDictionary();
			}
			StoreObjectId asStoreObjectId = IdConverter.GetAsStoreObjectId(storeId);
			string value = EWSSettings.DistinguishedFolderIdNameDictionary.Get(asStoreObjectId, session);
			if (!string.IsNullOrEmpty(value))
			{
				PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
				serviceObject[propertyInformation] = value;
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002BF2A File Offset: 0x0002A12A
		public static DistinguishedFolderIdProperty CreateCommand(CommandContext commandContext)
		{
			return new DistinguishedFolderIdProperty(commandContext);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0002BF32 File Offset: 0x0002A132
		public void ToXml()
		{
			throw new InvalidOperationException("DistinguishedFolderIdProperty.ToXml should not be called.");
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0002BF3E File Offset: 0x0002A13E
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("DistinguishedFolderIdProperty.ToXml should not be called.");
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0002BF4A File Offset: 0x0002A14A
		public override bool ToServiceObjectRequiresMailboxAccess
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0002BF50 File Offset: 0x0002A150
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreId id = commandSettings.StoreObject.Id;
			this.ToServiceObject(id, commandSettings.IdAndSession.Session, commandSettings.ServiceObject);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0002BF88 File Offset: 0x0002A188
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			StoreId storeId = null;
			if (PropertyCommand.TryGetValueFromPropertyBag<StoreId>(propertyBag, this.propertyDefinitions[0], out storeId))
			{
				this.ToServiceObject(storeId, commandSettings.IdAndSession.Session, commandSettings.ServiceObject);
			}
		}
	}
}
