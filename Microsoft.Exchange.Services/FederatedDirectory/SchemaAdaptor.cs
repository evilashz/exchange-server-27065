using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Server.Directory;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x0200007F RID: 127
	internal abstract class SchemaAdaptor
	{
		// Token: 0x0600033A RID: 826 RVA: 0x000102B4 File Offset: 0x0000E4B4
		public static void FromGroupMailboxToDirectoryObject(RequestSchema requestSchema, GetFederatedDirectoryGroupResponse groupMailbox, DirectoryObjectAccessor directoryObjectAccessor)
		{
			IEnumerable<SchemaAdaptor> adaptors = SchemaAdaptor.GetAdaptors(requestSchema);
			foreach (SchemaAdaptor schemaAdaptor in adaptors)
			{
				schemaAdaptor.FromGroupMailboxToDirectoryObject(groupMailbox, directoryObjectAccessor);
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00010304 File Offset: 0x0000E504
		public static void FromUserToDirectoryObject(RequestSchema requestSchema, GetFederatedDirectoryUserResponse user, DirectoryObjectAccessor directoryObjectAccessor)
		{
			IEnumerable<SchemaAdaptor> adaptors = SchemaAdaptor.GetAdaptors(requestSchema);
			foreach (SchemaAdaptor schemaAdaptor in adaptors)
			{
				schemaAdaptor.FromUserToDirectoryObject(user, directoryObjectAccessor);
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00010354 File Offset: 0x0000E554
		public static void FromDirectoryObjectToCmdletParameter(RequestSchema requestSchema, DirectoryObjectAccessor directoryObjectAccessor, NewGroupMailbox cmdlet)
		{
			IEnumerable<SchemaAdaptor> adaptors = SchemaAdaptor.GetAdaptors(requestSchema);
			foreach (SchemaAdaptor schemaAdaptor in adaptors)
			{
				schemaAdaptor.FromDirectoryObjectToCmdletParameter(directoryObjectAccessor, cmdlet);
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000103A4 File Offset: 0x0000E5A4
		public static void FromDirectoryObjectToCmdletParameter(RequestSchema requestSchema, DirectoryObjectAccessor directoryObjectAccessor, SetGroupMailbox cmdlet)
		{
			IEnumerable<SchemaAdaptor> adaptors = SchemaAdaptor.GetAdaptors(requestSchema);
			foreach (SchemaAdaptor schemaAdaptor in adaptors)
			{
				schemaAdaptor.FromDirectoryObjectToCmdletParameter(directoryObjectAccessor, cmdlet);
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000103F4 File Offset: 0x0000E5F4
		protected virtual void FromGroupMailboxToDirectoryObject(GetFederatedDirectoryGroupResponse groupMailbox, DirectoryObjectAccessor directoryObjectAccessor)
		{
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000103F6 File Offset: 0x0000E5F6
		protected virtual void FromUserToDirectoryObject(GetFederatedDirectoryUserResponse user, DirectoryObjectAccessor directoryObjectAccessor)
		{
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000103F8 File Offset: 0x0000E5F8
		protected virtual void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, NewGroupMailbox cmdlet)
		{
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000103FA File Offset: 0x0000E5FA
		protected virtual void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, SetGroupMailbox cmdlet)
		{
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000103FC File Offset: 0x0000E5FC
		private static T GetValueFromDirectoryObject<T>(DirectoryObjectAccessor directoryObjectAccessor, string propertyName) where T : class
		{
			Property property = directoryObjectAccessor.GetProperty(propertyName);
			if (property == null)
			{
				return default(T);
			}
			return property.Value as T;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001043B File Offset: 0x0000E63B
		protected static void FromIdentityDetailsToRelations(IList<FederatedDirectoryIdentityDetailsType> identitiesDetails, RelationSetAccessor relations)
		{
			if (identitiesDetails == null)
			{
				return;
			}
			relations.SetRelations(from id in identitiesDetails
			select new Guid(id.ExternalDirectoryObjectId));
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0001046C File Offset: 0x0000E66C
		protected static RecipientIdParameter[] GetIdsFromRelations(IEnumerable<Relation> relations)
		{
			if (relations == null)
			{
				return Array<RecipientIdParameter>.Empty;
			}
			List<RecipientIdParameter> list = new List<RecipientIdParameter>(10);
			foreach (Relation relation in relations)
			{
				list.Add(new RecipientIdParameter(relation.TargetObjectId.ToString()));
			}
			return list.ToArray();
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000104E4 File Offset: 0x0000E6E4
		private static IList<SchemaAdaptor> GetAdaptors(RequestSchema requestSchema)
		{
			if (requestSchema.IncludeAllProperties && requestSchema.IncludeAllResources && requestSchema.IncludeAllRelations)
			{
				return SchemaAdaptor.All;
			}
			List<SchemaAdaptor> list = new List<SchemaAdaptor>(10);
			list.Add(SchemaAdaptor.Id);
			if (requestSchema.IncludeAllProperties)
			{
				list.AddRange(SchemaAdaptor.Properties.Values);
			}
			else if (requestSchema.Properties != null)
			{
				foreach (string text in requestSchema.Properties)
				{
					SchemaAdaptor item;
					if (SchemaAdaptor.Properties.TryGetValue(text, out item))
					{
						list.Add(item);
					}
					else
					{
						SchemaAdaptor.Tracer.TraceError<string>(0L, "SchemaAdaptor.GetAdaptors() found unsupported property in RequestSchema: {0}", text);
					}
				}
			}
			if (requestSchema.IncludeAllResources)
			{
				list.AddRange(SchemaAdaptor.Resources.Values);
			}
			else if (requestSchema.Resources != null)
			{
				foreach (string text2 in requestSchema.Resources)
				{
					SchemaAdaptor item2;
					if (SchemaAdaptor.Resources.TryGetValue(text2, out item2))
					{
						list.Add(item2);
					}
					else
					{
						SchemaAdaptor.Tracer.TraceError<string>(0L, "SchemaAdaptor.GetAdaptors() found unsupported resource in RequestSchema: {0}", text2);
					}
				}
			}
			if (requestSchema.IncludeAllRelations)
			{
				list.AddRange(SchemaAdaptor.Relations.Values);
			}
			else if (requestSchema.Relations != null)
			{
				foreach (RelationRequestSchema relationRequestSchema in requestSchema.Relations)
				{
					SchemaAdaptor item3;
					if (SchemaAdaptor.Relations.TryGetValue(relationRequestSchema.Name, out item3))
					{
						list.Add(item3);
					}
					else
					{
						SchemaAdaptor.Tracer.TraceError<string>(0L, "SchemaAdaptor.GetAdaptors() found unsupported relation in RequestSchema: {0}", relationRequestSchema.Name);
					}
				}
			}
			return list;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000106C8 File Offset: 0x0000E8C8
		private static SchemaAdaptor[] MergeAdaptors(params IEnumerable<SchemaAdaptor>[] adaptorSets)
		{
			List<SchemaAdaptor> list = new List<SchemaAdaptor>(10);
			foreach (IEnumerable<SchemaAdaptor> collection in adaptorSets)
			{
				list.AddRange(collection);
			}
			return list.ToArray();
		}

		// Token: 0x040005C9 RID: 1481
		protected static readonly Trace Tracer = ExTraceGlobals.FederatedDirectoryTracer;

		// Token: 0x040005CA RID: 1482
		private static readonly SchemaAdaptor Id = new SchemaAdaptor.IdAdaptor();

		// Token: 0x040005CB RID: 1483
		private static readonly SchemaAdaptor MailProperty = new SchemaAdaptor.MailPropertyAdaptor();

		// Token: 0x040005CC RID: 1484
		private static readonly SchemaAdaptor AliasProperty = new SchemaAdaptor.AliasPropertyAdaptor();

		// Token: 0x040005CD RID: 1485
		private static readonly SchemaAdaptor DescriptionProperty = new SchemaAdaptor.DescriptionPropertyAdaptor();

		// Token: 0x040005CE RID: 1486
		private static readonly SchemaAdaptor DisplayNameProperty = new SchemaAdaptor.DisplayNamePropertyAdaptor();

		// Token: 0x040005CF RID: 1487
		private static readonly SchemaAdaptor AllowAccessToRelation = new SchemaAdaptor.AllowAccessToRelationAdaptor();

		// Token: 0x040005D0 RID: 1488
		private static readonly SchemaAdaptor MembersRelation = new SchemaAdaptor.MembersRelationAdaptor();

		// Token: 0x040005D1 RID: 1489
		private static readonly SchemaAdaptor OwnersRelation = new SchemaAdaptor.OwnersRelationAdaptor();

		// Token: 0x040005D2 RID: 1490
		private static readonly SchemaAdaptor MembershipRelation = new SchemaAdaptor.MembershipRelationAdaptor();

		// Token: 0x040005D3 RID: 1491
		private static readonly SchemaAdaptor SiteUrlResource = new SchemaAdaptor.SiteUrlResourceAdaptor();

		// Token: 0x040005D4 RID: 1492
		private static readonly Dictionary<string, SchemaAdaptor> Properties = new Dictionary<string, SchemaAdaptor>
		{
			{
				"Mail",
				SchemaAdaptor.MailProperty
			},
			{
				"Alias",
				SchemaAdaptor.AliasProperty
			},
			{
				"DisplayName",
				SchemaAdaptor.DisplayNameProperty
			},
			{
				"Description",
				SchemaAdaptor.DescriptionProperty
			}
		};

		// Token: 0x040005D5 RID: 1493
		private static readonly Dictionary<string, SchemaAdaptor> Resources = new Dictionary<string, SchemaAdaptor>
		{
			{
				"SiteUrl",
				SchemaAdaptor.SiteUrlResource
			}
		};

		// Token: 0x040005D6 RID: 1494
		private static readonly Dictionary<string, SchemaAdaptor> Relations = new Dictionary<string, SchemaAdaptor>
		{
			{
				"Members",
				SchemaAdaptor.MembersRelation
			},
			{
				"Owners",
				SchemaAdaptor.OwnersRelation
			},
			{
				"Membership",
				SchemaAdaptor.MembershipRelation
			},
			{
				"AllowAccessTo",
				SchemaAdaptor.AllowAccessToRelation
			}
		};

		// Token: 0x040005D7 RID: 1495
		private static readonly SchemaAdaptor[] Required = new SchemaAdaptor[]
		{
			SchemaAdaptor.Id
		};

		// Token: 0x040005D8 RID: 1496
		private static readonly SchemaAdaptor[] All = SchemaAdaptor.MergeAdaptors(new IEnumerable<SchemaAdaptor>[]
		{
			SchemaAdaptor.Required,
			SchemaAdaptor.Properties.Values,
			SchemaAdaptor.Resources.Values,
			SchemaAdaptor.Relations.Values
		});

		// Token: 0x02000080 RID: 128
		private sealed class IdAdaptor : SchemaAdaptor
		{
			// Token: 0x0600034A RID: 842 RVA: 0x00010894 File Offset: 0x0000EA94
			protected override void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, NewGroupMailbox cmdlet)
			{
				if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled)
				{
					return;
				}
				cmdlet.ExternalDirectoryObjectId = directoryObjectAccessor.DirectoryObject.Id.ToString();
				if (string.IsNullOrEmpty(cmdlet.Name))
				{
					Property property = directoryObjectAccessor.GetProperty("Alias");
					if (property != null)
					{
						string text = property.Value as string;
						if (!string.IsNullOrEmpty(text))
						{
							cmdlet.Name = text;
						}
					}
				}
			}

			// Token: 0x0600034B RID: 843 RVA: 0x00010918 File Offset: 0x0000EB18
			protected override void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, SetGroupMailbox cmdlet)
			{
				cmdlet.Identity = new RecipientIdParameter(directoryObjectAccessor.DirectoryObject.Id.ToString());
			}
		}

		// Token: 0x02000081 RID: 129
		private sealed class MailPropertyAdaptor : SchemaAdaptor
		{
			// Token: 0x0600034D RID: 845 RVA: 0x00010954 File Offset: 0x0000EB54
			protected override void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, NewGroupMailbox cmdlet)
			{
				string valueFromDirectoryObject = SchemaAdaptor.GetValueFromDirectoryObject<string>(directoryObjectAccessor, SchemaAdaptor.MailPropertyAdaptor.PropertyName);
				if (!string.IsNullOrEmpty(valueFromDirectoryObject))
				{
					cmdlet.PrimarySmtpAddress = new SmtpAddress(valueFromDirectoryObject);
				}
			}

			// Token: 0x040005DA RID: 1498
			private static readonly string PropertyName = "Mail";
		}

		// Token: 0x02000082 RID: 130
		private sealed class AliasPropertyAdaptor : SchemaAdaptor
		{
			// Token: 0x06000350 RID: 848 RVA: 0x00010998 File Offset: 0x0000EB98
			protected override void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, NewGroupMailbox cmdlet)
			{
				string valueFromDirectoryObject = SchemaAdaptor.GetValueFromDirectoryObject<string>(directoryObjectAccessor, SchemaAdaptor.AliasPropertyAdaptor.PropertyName);
				if (!string.IsNullOrEmpty(valueFromDirectoryObject))
				{
					cmdlet.Alias = valueFromDirectoryObject;
					cmdlet.Name = valueFromDirectoryObject;
				}
			}

			// Token: 0x06000351 RID: 849 RVA: 0x000109C8 File Offset: 0x0000EBC8
			protected override void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, SetGroupMailbox cmdlet)
			{
				Property property = directoryObjectAccessor.GetProperty(SchemaAdaptor.AliasPropertyAdaptor.PropertyName);
				if (property != null && property.IsModified)
				{
					string value = property.Value as string;
					if (!string.IsNullOrEmpty(value))
					{
						throw new NotImplementedException("Set-GroupMailbox does not allow to change Alias property yet");
					}
				}
			}

			// Token: 0x040005DB RID: 1499
			private static readonly string PropertyName = "Alias";
		}

		// Token: 0x02000083 RID: 131
		private sealed class DescriptionPropertyAdaptor : SchemaAdaptor
		{
			// Token: 0x06000354 RID: 852 RVA: 0x00010A20 File Offset: 0x0000EC20
			protected override void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, NewGroupMailbox cmdlet)
			{
				string valueFromDirectoryObject = SchemaAdaptor.GetValueFromDirectoryObject<string>(directoryObjectAccessor, SchemaAdaptor.DescriptionPropertyAdaptor.PropertyName);
				if (!string.IsNullOrEmpty(valueFromDirectoryObject))
				{
					cmdlet.Description = valueFromDirectoryObject;
				}
			}

			// Token: 0x06000355 RID: 853 RVA: 0x00010A48 File Offset: 0x0000EC48
			protected override void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, SetGroupMailbox cmdlet)
			{
				string valueFromDirectoryObject = SchemaAdaptor.GetValueFromDirectoryObject<string>(directoryObjectAccessor, SchemaAdaptor.DescriptionPropertyAdaptor.PropertyName);
				if (!string.IsNullOrEmpty(valueFromDirectoryObject))
				{
					cmdlet.Description = valueFromDirectoryObject;
				}
			}

			// Token: 0x040005DC RID: 1500
			private static readonly string PropertyName = "Description";
		}

		// Token: 0x02000084 RID: 132
		private sealed class DisplayNamePropertyAdaptor : SchemaAdaptor
		{
			// Token: 0x06000358 RID: 856 RVA: 0x00010A84 File Offset: 0x0000EC84
			protected override void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, NewGroupMailbox cmdlet)
			{
				string valueFromDirectoryObject = SchemaAdaptor.GetValueFromDirectoryObject<string>(directoryObjectAccessor, SchemaAdaptor.DisplayNamePropertyAdaptor.PropertyName);
				if (!string.IsNullOrEmpty(valueFromDirectoryObject))
				{
					cmdlet.DisplayName = valueFromDirectoryObject;
				}
			}

			// Token: 0x06000359 RID: 857 RVA: 0x00010AAC File Offset: 0x0000ECAC
			protected override void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, SetGroupMailbox cmdlet)
			{
				string valueFromDirectoryObject = SchemaAdaptor.GetValueFromDirectoryObject<string>(directoryObjectAccessor, SchemaAdaptor.DisplayNamePropertyAdaptor.PropertyName);
				if (valueFromDirectoryObject != null)
				{
					cmdlet.DisplayName = valueFromDirectoryObject;
				}
			}

			// Token: 0x040005DD RID: 1501
			private static readonly string PropertyName = "DisplayName";
		}

		// Token: 0x02000085 RID: 133
		private sealed class AllowAccessToRelationAdaptor : SchemaAdaptor
		{
			// Token: 0x0600035C RID: 860 RVA: 0x00010AE4 File Offset: 0x0000ECE4
			protected override void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, NewGroupMailbox cmdlet)
			{
				RelationSetAccessor relationSet = directoryObjectAccessor.GetRelationSet(SchemaAdaptor.AllowAccessToRelationAdaptor.RelationName);
				ModernGroupTypeInfo modernGroupType = ModernGroupTypeInfo.Public;
				if (relationSet != null && relationSet.RelationSet.Count == 0)
				{
					modernGroupType = ModernGroupTypeInfo.Private;
				}
				cmdlet.ModernGroupType = modernGroupType;
			}

			// Token: 0x040005DE RID: 1502
			private static readonly string RelationName = "AllowAccessTo";
		}

		// Token: 0x02000086 RID: 134
		private sealed class MembersRelationAdaptor : SchemaAdaptor
		{
			// Token: 0x0600035F RID: 863 RVA: 0x00010B2C File Offset: 0x0000ED2C
			protected override void FromGroupMailboxToDirectoryObject(GetFederatedDirectoryGroupResponse groupMailbox, DirectoryObjectAccessor directoryObjectAccessor)
			{
				RelationSetAccessor relationSet = directoryObjectAccessor.GetRelationSet(SchemaAdaptor.MembersRelationAdaptor.RelationName);
				if (relationSet != null)
				{
					SchemaAdaptor.FromIdentityDetailsToRelations(groupMailbox.Members, relationSet);
				}
			}

			// Token: 0x06000360 RID: 864 RVA: 0x00010B54 File Offset: 0x0000ED54
			protected override void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, NewGroupMailbox cmdlet)
			{
				RelationSetAccessor relationSet = directoryObjectAccessor.GetRelationSet(SchemaAdaptor.MembersRelationAdaptor.RelationName);
				if (relationSet != null)
				{
					cmdlet.Members = SchemaAdaptor.GetIdsFromRelations(relationSet.RelationSet);
				}
			}

			// Token: 0x06000361 RID: 865 RVA: 0x00010B84 File Offset: 0x0000ED84
			protected override void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, SetGroupMailbox cmdlet)
			{
				RelationSetAccessor relationSet = directoryObjectAccessor.GetRelationSet(SchemaAdaptor.MembersRelationAdaptor.RelationName);
				if (relationSet != null)
				{
					cmdlet.AddedMembers = SchemaAdaptor.GetIdsFromRelations(relationSet.AddedRelations);
					cmdlet.RemovedMembers = SchemaAdaptor.GetIdsFromRelations(relationSet.RemovedRelations);
				}
			}

			// Token: 0x040005DF RID: 1503
			private static readonly string RelationName = ExchangeDirectorySchema.MembersRelation.Name;
		}

		// Token: 0x02000087 RID: 135
		private sealed class OwnersRelationAdaptor : SchemaAdaptor
		{
			// Token: 0x06000364 RID: 868 RVA: 0x00010BDC File Offset: 0x0000EDDC
			protected override void FromGroupMailboxToDirectoryObject(GetFederatedDirectoryGroupResponse groupMailbox, DirectoryObjectAccessor directoryObjectAccessor)
			{
				RelationSetAccessor relationSet = directoryObjectAccessor.GetRelationSet(SchemaAdaptor.OwnersRelationAdaptor.RelationName);
				if (relationSet != null)
				{
					SchemaAdaptor.FromIdentityDetailsToRelations(groupMailbox.Owners, relationSet);
				}
			}

			// Token: 0x06000365 RID: 869 RVA: 0x00010C04 File Offset: 0x0000EE04
			protected override void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, NewGroupMailbox cmdlet)
			{
				RelationSetAccessor relationSet = directoryObjectAccessor.GetRelationSet(SchemaAdaptor.OwnersRelationAdaptor.RelationName);
				if (relationSet != null)
				{
					cmdlet.Owners = SchemaAdaptor.GetIdsFromRelations(relationSet.RelationSet);
				}
			}

			// Token: 0x06000366 RID: 870 RVA: 0x00010C34 File Offset: 0x0000EE34
			protected override void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, SetGroupMailbox cmdlet)
			{
				RelationSetAccessor relationSet = directoryObjectAccessor.GetRelationSet(SchemaAdaptor.OwnersRelationAdaptor.RelationName);
				if (relationSet != null)
				{
					cmdlet.AddOwners = SchemaAdaptor.GetIdsFromRelations(relationSet.AddedRelations);
					cmdlet.RemoveOwners = SchemaAdaptor.GetIdsFromRelations(relationSet.RemovedRelations);
				}
			}

			// Token: 0x040005E0 RID: 1504
			private static readonly string RelationName = ExchangeDirectorySchema.OwnersRelation.Name;
		}

		// Token: 0x02000088 RID: 136
		private sealed class MembershipRelationAdaptor : SchemaAdaptor
		{
			// Token: 0x06000369 RID: 873 RVA: 0x00010C8C File Offset: 0x0000EE8C
			protected override void FromUserToDirectoryObject(GetFederatedDirectoryUserResponse user, DirectoryObjectAccessor directoryObjectAccessor)
			{
				RelationSetAccessor relationSet = directoryObjectAccessor.GetRelationSet(SchemaAdaptor.MembershipRelationAdaptor.RelationName);
				if (relationSet != null && user.Groups != null && user.Groups.Length > 0)
				{
					foreach (FederatedDirectoryGroupType federatedDirectoryGroupType in user.Groups)
					{
						RelationAccessor relationAccessor = relationSet.AddRelation(new Guid(federatedDirectoryGroupType.ExternalDirectoryObjectId));
						relationAccessor.SetProperty(ExchangeDirectorySchema.JoinDateProperty.Name, (DateTime)federatedDirectoryGroupType.JoinDateTime, false);
					}
				}
			}

			// Token: 0x040005E1 RID: 1505
			private static readonly string RelationName = ExchangeDirectorySchema.MembershipRelation.Name;
		}

		// Token: 0x02000089 RID: 137
		private sealed class SiteUrlResourceAdaptor : SchemaAdaptor
		{
			// Token: 0x0600036C RID: 876 RVA: 0x00010D28 File Offset: 0x0000EF28
			protected override void FromDirectoryObjectToCmdletParameter(DirectoryObjectAccessor directoryObjectAccessor, SetGroupMailbox cmdlet)
			{
				Resource resource = directoryObjectAccessor.GetResource("SiteUrl");
				if (resource != null)
				{
					string text = resource.Value as string;
					if (!string.IsNullOrEmpty(text))
					{
						cmdlet.SharePointUrl = new Uri(text);
					}
				}
			}
		}
	}
}
