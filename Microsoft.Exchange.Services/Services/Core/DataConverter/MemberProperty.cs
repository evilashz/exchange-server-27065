using System;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000142 RID: 322
	internal class MemberProperty : MembersProperty
	{
		// Token: 0x060008DA RID: 2266 RVA: 0x0002B57C File Offset: 0x0002977C
		private MemberProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0002B588 File Offset: 0x00029788
		public static MemberProperty CreateMembersMemberCommand(CommandContext commandContext)
		{
			if (MembersProperty.RenderMembersCollection == null)
			{
				MembersProperty.RenderMembersCollection = new bool?(false);
			}
			return new MemberProperty(commandContext);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002B5B8 File Offset: 0x000297B8
		public override void ToServiceObject()
		{
			if (MembersProperty.RenderMembersCollection != null && !MembersProperty.RenderMembersCollection.Value)
			{
				ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
				ServiceObject serviceObject = commandSettings.ServiceObject;
				DictionaryPropertyUri dictionaryPropertyUri = commandSettings.PropertyPath as DictionaryPropertyUri;
				DistributionList distributionList = (DistributionList)commandSettings.StoreObject;
				if (distributionList.Count > 0)
				{
					string key = dictionaryPropertyUri.Key;
					int num = MembersProperty.FindMemberIndex(distributionList, key);
					if (num == -1)
					{
						throw new DistributionListMemberNotExistException(dictionaryPropertyUri);
					}
					MemberType memberType = base.MemberToServiceObject(distributionList, num);
					serviceObject[this.commandContext.PropertyInformation] = new MemberType[]
					{
						memberType
					};
				}
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0002B664 File Offset: 0x00029864
		public override void DeleteUpdate(DeletePropertyUpdate deletePropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			StoreObject storeObject = updateCommandSettings.StoreObject;
			DictionaryPropertyUri dictionaryPropertyUri = updateCommandSettings.PropertyUpdate.PropertyPath as DictionaryPropertyUri;
			DistributionList distributionList = (DistributionList)storeObject;
			int num = MembersProperty.FindMemberIndex(distributionList, dictionaryPropertyUri.Key);
			if (num == -1)
			{
				throw new DistributionListMemberNotExistException(dictionaryPropertyUri);
			}
			distributionList.RemoveAt(num);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002B6AF File Offset: 0x000298AF
		public override void AppendUpdate(AppendPropertyUpdate appendPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			throw new InvalidPropertyAppendException(appendPropertyUpdate.PropertyPath);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0002B6BC File Offset: 0x000298BC
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			throw new InvalidPropertySetException(setPropertyUpdate.PropertyPath);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0002B6CC File Offset: 0x000298CC
		public override void ToXml()
		{
			if (MembersProperty.RenderMembersCollection != null && !MembersProperty.RenderMembersCollection.Value)
			{
				ToXmlCommandSettings commandSettings = base.GetCommandSettings<ToXmlCommandSettings>();
				DictionaryPropertyUri dictionaryPropertyUri = commandSettings.PropertyPath as DictionaryPropertyUri;
				DistributionList distributionList = (DistributionList)commandSettings.StoreObject;
				if (distributionList.Count > 0)
				{
					string key = dictionaryPropertyUri.Key;
					int num = MembersProperty.FindMemberIndex(distributionList, key);
					if (num == -1)
					{
						throw new DistributionListMemberNotExistException(dictionaryPropertyUri);
					}
					XmlElement serviceItem = commandSettings.ServiceItem;
					XmlElement xmlElement = serviceItem["Members", "http://schemas.microsoft.com/exchange/services/2006/types"];
					if (xmlElement == null)
					{
						xmlElement = base.CreateXmlElement(commandSettings.ServiceItem, "Members");
					}
					base.MemberToXml(distributionList, num, xmlElement);
				}
			}
		}
	}
}
