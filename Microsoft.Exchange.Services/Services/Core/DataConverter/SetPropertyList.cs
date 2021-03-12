using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001B5 RID: 437
	internal sealed class SetPropertyList : PropertyList
	{
		// Token: 0x06000BD8 RID: 3032 RVA: 0x0003C2B0 File Offset: 0x0003A4B0
		public SetPropertyList(Shape shape, ServiceObject serviceObject, StoreObject storeObject, IdConverter idConverter) : base(shape)
		{
			this.serviceObject = serviceObject;
			this.storeObject = storeObject;
			this.CreatePropertyList(idConverter);
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0003C2CF File Offset: 0x0003A4CF
		private void CreatePropertyList(IdConverter idConverter)
		{
			this.commandContexts = new List<CommandContext>();
			this.BuildPropertyListFromShape(this.shape, idConverter);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0003C2EC File Offset: 0x0003A4EC
		private void BuildPropertyList(ServiceObject serviceObject, string path, Shape shape, IdConverter idConverter)
		{
			foreach (PropertyInformation propertyInformation in serviceObject.LoadedProperties)
			{
				PropertyInformation propertyInformation2;
				if (shape.Schema.TryGetPropertyInformationByPath(propertyInformation.PropertyPath, out propertyInformation2))
				{
					if (!propertyInformation.ImplementsSetCommand)
					{
						throw new InvalidPropertySetException(propertyInformation.PropertyPath);
					}
					this.commandContexts.Add(new CommandContext(new SetCommandSettings(serviceObject, this.storeObject), propertyInformation, idConverter));
				}
			}
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0003C384 File Offset: 0x0003A584
		public IList<ISetCommand> CreatePropertyCommands()
		{
			List<ISetCommand> list = new List<ISetCommand>();
			foreach (CommandContext commandContext in this.commandContexts)
			{
				list.Add((ISetCommand)commandContext.PropertyInformation.CreatePropertyCommand(commandContext));
			}
			return list;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0003C3F4 File Offset: 0x0003A5F4
		public SetPropertyList(Shape shape, XmlElement serviceItem, StoreObject storeObject, IdConverter idConverter) : base(shape)
		{
			this.serviceItem = serviceItem;
			this.storeObject = storeObject;
			this.CreatePropertyList(idConverter);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0003C414 File Offset: 0x0003A614
		private void BuildPropertyListFromShape(Shape shape, IdConverter idConverter)
		{
			if (shape.InnerShape != null)
			{
				this.BuildPropertyListFromShape(shape.InnerShape, idConverter);
			}
			if (this.serviceObject != null)
			{
				this.BuildPropertyList(this.serviceObject, Schema.RootXmlElementPath, shape, idConverter);
				return;
			}
			this.BuildPropertyList(this.serviceItem, Schema.RootXmlElementPath, shape, idConverter);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0003C468 File Offset: 0x0003A668
		private void BuildPropertyList(XmlElement xmlContainer, string path, Shape shape, IdConverter idConverter)
		{
			foreach (object obj in xmlContainer.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement != null)
				{
					string path2 = Schema.BuildXmlElementPath(xmlElement, path);
					XmlElementInformation xmlElementInformation = null;
					if (shape.Schema.TryGetXmlElementInformationByPath(path2, out xmlElementInformation))
					{
						PropertyInformation propertyInformation = xmlElementInformation as PropertyInformation;
						if (propertyInformation != null)
						{
							if (!propertyInformation.ImplementsSetCommand)
							{
								throw new InvalidPropertySetException(propertyInformation.PropertyPath);
							}
							this.commandContexts.Add(new CommandContext(new SetCommandSettings(xmlElement, this.storeObject), propertyInformation, idConverter));
						}
						else if (xmlElementInformation is ContainerInformation)
						{
							this.BuildPropertyList(xmlElement, path2, shape, idConverter);
						}
					}
				}
			}
		}

		// Token: 0x04000969 RID: 2409
		private List<CommandContext> commandContexts;

		// Token: 0x0400096A RID: 2410
		private ServiceObject serviceObject;

		// Token: 0x0400096B RID: 2411
		private StoreObject storeObject;

		// Token: 0x0400096C RID: 2412
		private XmlElement serviceItem;
	}
}
