using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000141 RID: 321
	internal class MembersProperty : ComplexPropertyBase, IPregatherParticipants, IToXmlCommand, IToServiceObjectCommand, ISetCommand, ISetUpdateCommand, IAppendUpdateCommand, IDeleteUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x060008C7 RID: 2247 RVA: 0x0002AFBB File Offset: 0x000291BB
		public MembersProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0002AFC4 File Offset: 0x000291C4
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x0002AFCB File Offset: 0x000291CB
		protected static bool? RenderMembersCollection
		{
			get
			{
				return MembersProperty.renderMembersCollection;
			}
			set
			{
				MembersProperty.renderMembersCollection = value;
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0002AFD4 File Offset: 0x000291D4
		public static MembersProperty CreateMembersCommand(CommandContext commandContext)
		{
			MembersProperty result = new MembersProperty(commandContext);
			MembersProperty.renderMembersCollection = new bool?(true);
			return result;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0002AFF4 File Offset: 0x000291F4
		void IPregatherParticipants.Pregather(StoreObject storeObject, List<Participant> participants)
		{
			DistributionList distributionList = (DistributionList)storeObject;
			if (distributionList.Count > 0)
			{
				for (int i = 0; i < distributionList.Count; i++)
				{
					participants.Add(distributionList[i].Participant);
				}
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0002B034 File Offset: 0x00029234
		public virtual void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			DistributionListType distributionListType = commandSettings.ServiceObject as DistributionListType;
			DistributionList distributionList = (DistributionList)commandSettings.StoreObject;
			if (distributionList.Count > 0)
			{
				bool flag = true;
				foreach (DistributionListMember distributionListMember in distributionList)
				{
					if (distributionListMember.Participant != null)
					{
						flag = false;
						break;
					}
				}
				if (!flag)
				{
					List<MemberType> list = new List<MemberType>();
					for (int i = 0; i < distributionList.Count; i++)
					{
						if (distributionList[i].Participant != null)
						{
							MemberType item = this.MemberToServiceObject(distributionList, i);
							list.Add(item);
						}
					}
					distributionListType.Members = list.ToArray();
				}
			}
			MembersProperty.RenderMembersCollection = null;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002B120 File Offset: 0x00029320
		public void Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			this.SetProperty(serviceObject, storeObject, false);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0002B14C File Offset: 0x0002934C
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			StoreObject storeObject = updateCommandSettings.StoreObject;
			ServiceObject serviceObject = setPropertyUpdate.ServiceObject;
			this.SetProperty(serviceObject, storeObject, false);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002B170 File Offset: 0x00029370
		public override void DeleteUpdate(DeletePropertyUpdate deletePropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			StoreObject storeObject = updateCommandSettings.StoreObject;
			DistributionList distributionList = (DistributionList)storeObject;
			distributionList.Clear();
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002B194 File Offset: 0x00029394
		public override void AppendUpdate(AppendPropertyUpdate appendPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			StoreObject storeObject = updateCommandSettings.StoreObject;
			ServiceObject serviceObject = appendPropertyUpdate.ServiceObject;
			this.SetProperty(serviceObject, storeObject, true);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002B1B8 File Offset: 0x000293B8
		protected static int FindMemberIndex(DistributionList distributionList, string memberKey)
		{
			for (int i = 0; i < distributionList.Count; i++)
			{
				if (distributionList[i] != null)
				{
					ParticipantEntryId mainEntryId = distributionList[i].MainEntryId;
					string text = MembersProperty.MemberKeyToString(mainEntryId.ToByteArray());
					if (text != null && text == memberKey)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002B208 File Offset: 0x00029408
		protected MemberType MemberToServiceObject(DistributionList distributionList, int i)
		{
			MemberType memberType = new MemberType();
			DistributionListMember distributionListMember = distributionList[i];
			ParticipantInformation participant = EWSSettings.ParticipantInformation.GetParticipant(distributionListMember.Participant);
			memberType.Key = MembersProperty.MemberKeyToString(distributionListMember.MainEntryId.ToByteArray());
			memberType.StatusString = ((participant.Demoted != null && participant.Demoted.Value) ? MemberStatus.Demoted.ToString() : distributionListMember.MemberStatus.ToString());
			memberType.Mailbox = base.CreateRecipientFromParticipant(participant, distributionList).Mailbox;
			return memberType;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0002B2A4 File Offset: 0x000294A4
		protected void SetProperty(ServiceObject serviceObject, StoreObject storeObject, bool appendMode)
		{
			DistributionList distributionList = (DistributionList)storeObject;
			if (!appendMode)
			{
				distributionList.Clear();
			}
			MemberType[] valueOrDefault = serviceObject.GetValueOrDefault<MemberType[]>(this.commandContext.PropertyInformation);
			if (valueOrDefault != null)
			{
				for (int i = 0; i < valueOrDefault.Length; i++)
				{
					MemberType memberType = valueOrDefault[i];
					if (memberType.Mailbox != null)
					{
						try
						{
							Participant participant2;
							Participant participant = MailboxHelper.ParseMailbox(this.commandContext.PropertyInformation.PropertyPath, distributionList, memberType.Mailbox, this.commandContext.IdConverter, out participant2, false);
							this.AddMember(distributionList, participant);
						}
						catch (ServicePermanentExceptionWithPropertyPath servicePermanentExceptionWithPropertyPath)
						{
							servicePermanentExceptionWithPropertyPath.ConstantValues.Add("MemberIndex", i.ToString());
							throw servicePermanentExceptionWithPropertyPath;
						}
						catch (LocalizedException innerException)
						{
							throw new InvalidMailboxException(i, this.commandContext.PropertyInformation.PropertyPath, innerException, CoreResources.IDs.ErrorInvalidMailbox);
						}
					}
				}
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0002B390 File Offset: 0x00029590
		protected void AddMember(DistributionList distributionList, Participant participant)
		{
			int count = distributionList.Count;
			distributionList.Add(participant);
			ParticipantEntryId mainEntryId = distributionList[count].MainEntryId;
			string memberKey = MembersProperty.MemberKeyToString(mainEntryId.ToByteArray());
			int num = MembersProperty.FindMemberIndex(distributionList, memberKey);
			if (num < count)
			{
				distributionList.RemoveAt(count);
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002B3D8 File Offset: 0x000295D8
		private static string MemberKeyToString(byte[] memberKey)
		{
			return Convert.ToBase64String(memberKey);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002B3E0 File Offset: 0x000295E0
		public virtual void ToXml()
		{
			ToXmlCommandSettings commandSettings = base.GetCommandSettings<ToXmlCommandSettings>();
			DistributionList distributionList = (DistributionList)commandSettings.StoreObject;
			if (distributionList.Count > 0)
			{
				bool flag = true;
				foreach (DistributionListMember distributionListMember in distributionList)
				{
					if (distributionListMember.Participant != null)
					{
						flag = false;
						break;
					}
				}
				if (!flag)
				{
					XmlElement xmlParent = base.CreateXmlElement(commandSettings.ServiceItem, "Members");
					for (int i = 0; i < distributionList.Count; i++)
					{
						if (distributionList[i].Participant != null)
						{
							this.MemberToXml(distributionList, i, xmlParent);
						}
					}
				}
			}
			MembersProperty.RenderMembersCollection = null;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0002B4B4 File Offset: 0x000296B4
		protected void MemberToXml(DistributionList distributionList, int index, XmlElement xmlParent)
		{
			DistributionListMember distributionListMember = distributionList[index];
			XmlElement parentElement = base.CreateXmlElement(xmlParent, "Member");
			ParticipantEntryId mainEntryId = distributionListMember.MainEntryId;
			string attributeValue = MembersProperty.MemberKeyToString(mainEntryId.ToByteArray());
			PropertyCommand.CreateXmlAttribute(parentElement, "Key", attributeValue);
			ParticipantInformation participant = EWSSettings.ParticipantInformation.GetParticipant(distributionListMember.Participant);
			base.CreateParticipantOrDLXml(parentElement, participant, distributionList);
			string textValue = (participant.Demoted != null && participant.Demoted.Value) ? MemberStatus.Demoted.ToString() : distributionListMember.MemberStatus.ToString();
			base.CreateXmlTextElement(parentElement, "Status", textValue);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0002B563 File Offset: 0x00029763
		protected void SetProperty(XmlElement xmlMembers, StoreObject storeObject, bool appendMode)
		{
			throw new InvalidOperationException("MembersProperty.SetProperty for XML should not be called.");
		}

		// Token: 0x04000771 RID: 1905
		protected const string XmlElementNameMembers = "Members";

		// Token: 0x04000772 RID: 1906
		private const string XmlElementNameMember = "Member";

		// Token: 0x04000773 RID: 1907
		private const string XmlAttributeNameKey = "Key";

		// Token: 0x04000774 RID: 1908
		private const string XmlElementNameMemberStatus = "Status";

		// Token: 0x04000775 RID: 1909
		private const string XmlElementNameMailbox = "Mailbox";

		// Token: 0x04000776 RID: 1910
		[ThreadStatic]
		private static bool? renderMembersCollection = null;
	}
}
