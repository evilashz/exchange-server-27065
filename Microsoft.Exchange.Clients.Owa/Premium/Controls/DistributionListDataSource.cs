using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000353 RID: 851
	internal class DistributionListDataSource : ExchangeListViewDataSource, IListViewDataSource
	{
		// Token: 0x0600201F RID: 8223 RVA: 0x000BA380 File Offset: 0x000B8580
		internal DistributionListDataSource(UserContext userContext, Hashtable properties, List<Participant> participants, ColumnId sortedColumn, SortOrder order) : base(properties)
		{
			using (List<Participant>.Enumerator enumerator = participants.GetEnumerator())
			{
				AdRecipientBatchQuery adRecipientBatchQuery = new AdRecipientBatchQuery(enumerator, userContext);
				PropertyDefinition[] requestedProperties = base.GetRequestedProperties();
				object[][] array = new object[participants.Count][];
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				for (int i = 0; i < participants.Count; i++)
				{
					array[i] = new object[requestedProperties.Length];
					array[i][base.PropertyIndex(StoreObjectSchema.DisplayName)] = participants[i].DisplayName;
					array[i][base.PropertyIndex(ParticipantSchema.EmailAddress)] = participants[i].EmailAddress;
					array[i][base.PropertyIndex(ParticipantSchema.RoutingType)] = participants[i].RoutingType;
					string text2;
					if (participants[i].Origin is StoreParticipantOrigin)
					{
						string text = ((StoreParticipantOrigin)participants[i].Origin).OriginItemId.ToBase64String();
						if (Utilities.IsMapiPDL(participants[i].RoutingType))
						{
							array[i][base.PropertyIndex(StoreObjectSchema.ItemClass)] = "IPM.DistList";
							array[i][base.PropertyIndex(RecipientSchema.EmailAddrType)] = 0;
							text2 = text;
						}
						else
						{
							int emailAddressIndex = (int)((StoreParticipantOrigin)participants[i].Origin).EmailAddressIndex;
							array[i][base.PropertyIndex(StoreObjectSchema.ItemClass)] = "IPM.Contact";
							array[i][base.PropertyIndex(RecipientSchema.EmailAddrType)] = emailAddressIndex;
							text2 = text + emailAddressIndex;
						}
						array[i][base.PropertyIndex(ItemSchema.RecipientType)] = AddressOrigin.Store;
						array[i][base.PropertyIndex(ContactSchema.Email1)] = participants[i];
						array[i][base.PropertyIndex(ParticipantSchema.OriginItemId)] = text;
					}
					else if (participants[i].Origin is DirectoryParticipantOrigin)
					{
						ADRecipient adRecipient = adRecipientBatchQuery.GetAdRecipient(participants[i].EmailAddress);
						if (adRecipient != null)
						{
							if (Utilities.IsADDistributionList(adRecipient.RecipientType))
							{
								array[i][base.PropertyIndex(StoreObjectSchema.ItemClass)] = "AD.RecipientType.Group";
							}
							else
							{
								array[i][base.PropertyIndex(StoreObjectSchema.ItemClass)] = "AD.RecipientType.User";
							}
							array[i][base.PropertyIndex(ContactSchema.Email1)] = new Participant(participants[i].DisplayName, adRecipient.PrimarySmtpAddress.ToString(), "SMTP");
							text2 = Utilities.GetBase64StringFromADObjectId(adRecipient.Id);
						}
						else
						{
							text2 = "[ADUser]" + participants[i].DisplayName + participants[i].EmailAddress;
							array[i][base.PropertyIndex(StoreObjectSchema.ItemClass)] = "AD.RecipientType.User";
							array[i][base.PropertyIndex(ContactSchema.Email1)] = participants[i];
							string participantProperty = Utilities.GetParticipantProperty<string>(participants[i], ParticipantSchema.SmtpAddress, null);
							if (participantProperty != null)
							{
								array[i][base.PropertyIndex(ParticipantSchema.RoutingType)] = "SMTP";
								array[i][base.PropertyIndex(ParticipantSchema.EmailAddress)] = participantProperty;
							}
						}
						array[i][base.PropertyIndex(ItemSchema.RecipientType)] = AddressOrigin.Directory;
					}
					else
					{
						if (!(participants[i].Origin is OneOffParticipantOrigin))
						{
							throw new ArgumentException("Invalid participant origin type.");
						}
						text2 = participants[i].RoutingType + ":" + participants[i].EmailAddress;
						array[i][base.PropertyIndex(StoreObjectSchema.ItemClass)] = "OneOff";
						array[i][base.PropertyIndex(ItemSchema.RecipientType)] = AddressOrigin.OneOff;
						array[i][base.PropertyIndex(ContactSchema.Email1)] = participants[i];
					}
					if (dictionary.ContainsKey(text2))
					{
						text2 += i;
					}
					dictionary[text2] = 1;
					array[i][base.PropertyIndex(ItemSchema.Id)] = text2;
				}
				Array.Sort<object[]>(array, new DistributionListDataSource.DistributionListMemberComparer(userContext.UserCulture, sortedColumn, order, this));
				base.Items = array;
				base.StartRange = 0;
				base.EndRange = array.Length - 1;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x000BA7DC File Offset: 0x000B89DC
		public string ContainerId
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x000BA7E3 File Offset: 0x000B89E3
		public string GetItemClass()
		{
			return this.GetItemProperty<string>(StoreObjectSchema.ItemClass);
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x000BA7F0 File Offset: 0x000B89F0
		public string GetItemId()
		{
			return this.GetItemProperty<string>(ItemSchema.Id);
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x000BA7FD File Offset: 0x000B89FD
		public void Load(ObjectId seekToObjectId, SeekDirection seekDirection, int itemCount)
		{
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x000BA7FF File Offset: 0x000B89FF
		public void Load(int startRange, int itemCount)
		{
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x000BA801 File Offset: 0x000B8A01
		public void Load(string seekValue, int itemCount)
		{
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000BA803 File Offset: 0x000B8A03
		public bool LoadAdjacent(ObjectId adjacentObjectId, SeekDirection seekDirection, int itemCount)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x000BA80A File Offset: 0x000B8A0A
		public override int TotalCount
		{
			get
			{
				return base.RangeCount;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x000BA812 File Offset: 0x000B8A12
		public int UnreadCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x000BA815 File Offset: 0x000B8A15
		public bool UserHasRightToLoad
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001747 RID: 5959
		public const string OneOffContact = "OneOff";

		// Token: 0x02000354 RID: 852
		private class DistributionListMemberComparer : IComparer<object[]>
		{
			// Token: 0x0600202A RID: 8234 RVA: 0x000BA818 File Offset: 0x000B8A18
			public DistributionListMemberComparer(CultureInfo cultureInfo, ColumnId sortedColumn, SortOrder order, DistributionListDataSource dataSource)
			{
				this.cultureInfo = cultureInfo;
				this.sortedColumn = sortedColumn;
				this.order = order;
				this.dataSource = dataSource;
			}

			// Token: 0x0600202B RID: 8235 RVA: 0x000BA840 File Offset: 0x000B8A40
			int IComparer<object[]>.Compare(object[] x, object[] y)
			{
				int num = 0;
				if (this.sortedColumn == ColumnId.MemberIcon)
				{
					num = string.Compare((string)x[this.dataSource.PropertyIndex(StoreObjectSchema.ItemClass)], (string)y[this.dataSource.PropertyIndex(StoreObjectSchema.ItemClass)]);
				}
				else
				{
					string strA;
					string strA2;
					ContactUtilities.GetParticipantEmailAddress((Participant)x[this.dataSource.PropertyIndex(ContactSchema.Email1)], out strA, out strA2);
					string strB;
					string strB2;
					ContactUtilities.GetParticipantEmailAddress((Participant)y[this.dataSource.PropertyIndex(ContactSchema.Email1)], out strB, out strB2);
					switch (this.sortedColumn)
					{
					case ColumnId.MemberDisplayName:
						num = string.Compare(strA2, strB2, false, this.cultureInfo);
						break;
					case ColumnId.MemberEmail:
						num = string.Compare(strA, strB, false, this.cultureInfo);
						break;
					}
				}
				if (this.order == SortOrder.Ascending)
				{
					return num;
				}
				return -num;
			}

			// Token: 0x04001748 RID: 5960
			private readonly CultureInfo cultureInfo;

			// Token: 0x04001749 RID: 5961
			private readonly ColumnId sortedColumn;

			// Token: 0x0400174A RID: 5962
			private readonly SortOrder order;

			// Token: 0x0400174B RID: 5963
			private readonly DistributionListDataSource dataSource;
		}
	}
}
