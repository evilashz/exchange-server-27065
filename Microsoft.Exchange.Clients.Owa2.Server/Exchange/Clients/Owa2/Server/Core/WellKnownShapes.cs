using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000422 RID: 1058
	public static class WellKnownShapes
	{
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x0600240F RID: 9231 RVA: 0x000815C0 File Offset: 0x0007F7C0
		public static DistinguishedFolderIdName[] RequiredDistinguishedFolders
		{
			get
			{
				return WellKnownShapes.requiredDistinguishedFolders.ToArray();
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x000815CC File Offset: 0x0007F7CC
		public static DistinguishedFolderIdName[] FoldersToMoveToTop
		{
			get
			{
				return WellKnownShapes.foldersToMoveToTop.ToArray();
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x000815D8 File Offset: 0x0007F7D8
		public static Dictionary<WellKnownShapeName, ResponseShape> ResponseShapes
		{
			get
			{
				return WellKnownShapes.responseShapes.Member;
			}
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000815E4 File Offset: 0x0007F7E4
		public static string GenerateRandomCssScopeName()
		{
			Random random = new Random();
			return "rps_" + ((int)Math.Floor(random.NextDouble() * 65535.0 + 1.0)).ToString("X4");
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x00081630 File Offset: 0x0007F830
		private static FolderResponseShape Folder
		{
			get
			{
				return new FolderResponseShape
				{
					BaseShape = ShapeEnum.IdOnly,
					AdditionalProperties = new PropertyPath[]
					{
						new PropertyUri(PropertyUriEnum.FolderId),
						new PropertyUri(PropertyUriEnum.ParentFolderId),
						new PropertyUri(PropertyUriEnum.FolderDisplayName),
						new PropertyUri(PropertyUriEnum.UnreadCount),
						new PropertyUri(PropertyUriEnum.TotalCount),
						new PropertyUri(PropertyUriEnum.ChildFolderCount),
						new PropertyUri(PropertyUriEnum.FolderClass),
						new PropertyUri(PropertyUriEnum.FolderEffectiveRights),
						new PropertyUri(PropertyUriEnum.DistinguishedFolderId),
						new PropertyUri(PropertyUriEnum.FolderPolicyTag),
						new PropertyUri(PropertyUriEnum.FolderArchiveTag),
						WellKnownProperties.Hidden,
						WellKnownProperties.RetentionFlags,
						new PropertyUri(PropertyUriEnum.UnClutteredViewFolderEntryId),
						new PropertyUri(PropertyUriEnum.ClutteredViewFolderEntryId),
						new PropertyUri(PropertyUriEnum.ClutterCount),
						new PropertyUri(PropertyUriEnum.UnreadClutterCount),
						WellKnownProperties.WorkingSetSourcePartitionInternal
					}
				};
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002414 RID: 9236 RVA: 0x0008170C File Offset: 0x0007F90C
		private static ItemResponseShape ItemPartUniqueBody
		{
			get
			{
				ItemResponseShape itemResponseShape = WellKnownShapes.CreateExpandedShapeFromProperties<ItemResponseShape>(WellKnownShapes.CommonMailReadingPaneShape, new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.UniqueBody),
					new PropertyUri(PropertyUriEnum.IsGroupEscalationMessage)
				});
				itemResponseShape.FlightedProperties = new Dictionary<string, PropertyPath[]>
				{
					{
						"Like",
						new PropertyPath[]
						{
							new PropertyUri(PropertyUriEnum.LikeCount),
							new PropertyUri(PropertyUriEnum.Likers)
						}
					},
					{
						"SuperSort",
						new PropertyPath[]
						{
							new PropertyUri(PropertyUriEnum.ReceivedOrRenewTime)
						}
					}
				};
				return itemResponseShape;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002415 RID: 9237 RVA: 0x0008179C File Offset: 0x0007F99C
		private static ItemResponseShape ItemPartNormalizedBody
		{
			get
			{
				return WellKnownShapes.CreateExpandedShape<ItemResponseShape>(WellKnownShapes.ItemPartUniqueBody, new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.NormalizedBody)
				});
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x000817C8 File Offset: 0x0007F9C8
		private static ItemResponseShape ItemNormalizedBody
		{
			get
			{
				ItemResponseShape itemResponseShape = WellKnownShapes.CreateExpandedShapeFromProperties<ItemResponseShape>(WellKnownShapes.CommonMailReadingPaneShape, new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.NormalizedBody),
					new PropertyUri(PropertyUriEnum.IsGroupEscalationMessage)
				});
				itemResponseShape.FlightedProperties = new Dictionary<string, PropertyPath[]>
				{
					{
						"SuperSort",
						new PropertyPath[]
						{
							new PropertyUri(PropertyUriEnum.ReceivedOrRenewTime)
						}
					}
				};
				return itemResponseShape;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x00081827 File Offset: 0x0007FA27
		private static AttachmentResponseShape ItemAttachment
		{
			get
			{
				return WellKnownShapes.CreateItemAttachmentBaseResponseShape();
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x00081830 File Offset: 0x0007FA30
		private static List<PropertyPath> FindConversationBasePropertySet
		{
			get
			{
				return new List<PropertyPath>
				{
					new PropertyUri(PropertyUriEnum.ConversationGuidId),
					new PropertyUri(PropertyUriEnum.Topic),
					new PropertyUri(PropertyUriEnum.ConversationLastDeliveryTime),
					new PropertyUri(PropertyUriEnum.ConversationCategories),
					new PropertyUri(PropertyUriEnum.ConversationFlagStatus),
					new PropertyUri(PropertyUriEnum.ConversationHasAttachments),
					new PropertyUri(PropertyUriEnum.ConversationMessageCount),
					new PropertyUri(PropertyUriEnum.ConversationGlobalMessageCount),
					new PropertyUri(PropertyUriEnum.ConversationUnreadCount),
					new PropertyUri(PropertyUriEnum.ConversationGlobalUnreadCount),
					new PropertyUri(PropertyUriEnum.ConversationSize),
					new PropertyUri(PropertyUriEnum.ConversationItemClasses),
					new PropertyUri(PropertyUriEnum.ConversationImportance),
					new PropertyUri(PropertyUriEnum.ConversationItemIds),
					new PropertyUri(PropertyUriEnum.ConversationGlobalItemIds),
					new PropertyUri(PropertyUriEnum.ConversationLastModifiedTime),
					new PropertyUri(PropertyUriEnum.ConversationInstanceKey),
					new PropertyUri(PropertyUriEnum.ConversationPreview),
					new PropertyUri(PropertyUriEnum.ConversationGlobalIconIndex),
					new PropertyUri(PropertyUriEnum.ConversationIconIndex),
					new PropertyUri(PropertyUriEnum.ConversationDraftItemIds),
					new PropertyUri(PropertyUriEnum.ConversationHasIrm)
				};
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x000819A4 File Offset: 0x0007FBA4
		private static ConversationResponseShape FindConversationBaseResponseShape
		{
			get
			{
				return new ConversationResponseShape(ShapeEnum.IdOnly, WellKnownShapes.FindConversationBasePropertySet.ToArray())
				{
					FlightedProperties = new Dictionary<string, PropertyPath[]>
					{
						{
							"SuperSort",
							new PropertyPath[]
							{
								new PropertyUri(PropertyUriEnum.ConversationLastDeliveryOrRenewTime)
							}
						}
					}
				};
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x000819F0 File Offset: 0x0007FBF0
		private static ConversationResponseShape FindConversationNormalShape
		{
			get
			{
				return WellKnownShapes.CreateExpandedShape<ConversationResponseShape>(WellKnownShapes.FindConversationBaseResponseShape, new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.ConversationUniqueSenders)
				});
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x0600241B RID: 9243 RVA: 0x00081A1C File Offset: 0x0007FC1C
		private static ConversationResponseShape FindConversationSentItemsShape
		{
			get
			{
				return WellKnownShapes.CreateExpandedShape<ConversationResponseShape>(WellKnownShapes.FindConversationBaseResponseShape, new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.ConversationUniqueRecipients)
				});
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x00081A48 File Offset: 0x0007FC48
		private static ConversationResponseShape FindConversationUberShape
		{
			get
			{
				return WellKnownShapes.CreateExpandedShape<ConversationResponseShape>(WellKnownShapes.FindConversationBaseResponseShape, new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.ConversationUniqueSenders),
					new PropertyUri(PropertyUriEnum.ConversationUniqueRecipients)
				});
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x00081A84 File Offset: 0x0007FC84
		private static ConversationResponseShape InferenceFindConversationNormalShape
		{
			get
			{
				return WellKnownShapes.CreateExpandedShape<ConversationResponseShape>(WellKnownShapes.FindConversationNormalShape, new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.ConversationHasClutter)
				});
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x00081AB0 File Offset: 0x0007FCB0
		private static ConversationResponseShape InferenceFindConversationUberShape
		{
			get
			{
				return WellKnownShapes.CreateExpandedShape<ConversationResponseShape>(WellKnownShapes.FindConversationUberShape, new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.ConversationHasClutter)
				});
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x0600241F RID: 9247 RVA: 0x00081ADC File Offset: 0x0007FCDC
		private static ItemResponseShape DiscoveryItemShape
		{
			get
			{
				return WellKnownShapes.CreateExpandedShapeFromProperties<ItemResponseShape>(WellKnownShapes.CommonMailReadingPaneEwsShape, new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.NormalizedBody)
				});
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002420 RID: 9248 RVA: 0x00081B08 File Offset: 0x0007FD08
		private static ConversationResponseShape GroupConversationListView
		{
			get
			{
				return new ConversationResponseShape(ShapeEnum.IdOnly, new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.ConversationGuidId),
					new PropertyUri(PropertyUriEnum.Topic),
					new PropertyUri(PropertyUriEnum.ConversationLastDeliveryTime),
					new PropertyUri(PropertyUriEnum.ConversationCategories),
					new PropertyUri(PropertyUriEnum.ConversationHasAttachments),
					new PropertyUri(PropertyUriEnum.ConversationMessageCount),
					new PropertyUri(PropertyUriEnum.ConversationGlobalMessageCount),
					new PropertyUri(PropertyUriEnum.ConversationItemClasses),
					new PropertyUri(PropertyUriEnum.ConversationImportance),
					new PropertyUri(PropertyUriEnum.ConversationItemIds),
					new PropertyUri(PropertyUriEnum.ConversationGlobalItemIds),
					new PropertyUri(PropertyUriEnum.ConversationLastModifiedTime),
					new PropertyUri(PropertyUriEnum.ConversationInstanceKey),
					new PropertyUri(PropertyUriEnum.ConversationIconIndex),
					new PropertyUri(PropertyUriEnum.ConversationGlobalIconIndex),
					new PropertyUri(PropertyUriEnum.ConversationPreview),
					new PropertyUri(PropertyUriEnum.ConversationHasIrm),
					new PropertyUri(PropertyUriEnum.ConversationUniqueSenders)
				});
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x00081C18 File Offset: 0x0007FE18
		private static ConversationResponseShape GroupConversationFeedView
		{
			get
			{
				ConversationResponseShape conversationResponseShape = new ConversationResponseShape();
				conversationResponseShape.BaseShape = ShapeEnum.IdOnly;
				List<PropertyPath> list = new List<PropertyPath>
				{
					new PropertyUri(PropertyUriEnum.ConversationGuidId),
					new PropertyUri(PropertyUriEnum.Topic),
					new PropertyUri(PropertyUriEnum.ConversationLastDeliveryTime),
					new PropertyUri(PropertyUriEnum.ConversationCategories),
					new PropertyUri(PropertyUriEnum.ConversationLastModifiedTime),
					new PropertyUri(PropertyUriEnum.ConversationHasAttachments),
					new PropertyUri(PropertyUriEnum.ConversationMessageCount),
					new PropertyUri(PropertyUriEnum.ConversationGlobalMessageCount),
					new PropertyUri(PropertyUriEnum.ConversationItemClasses),
					new PropertyUri(PropertyUriEnum.ConversationImportance),
					new PropertyUri(PropertyUriEnum.ConversationItemIds),
					new PropertyUri(PropertyUriEnum.ConversationGlobalItemIds),
					new PropertyUri(PropertyUriEnum.ConversationLastModifiedTime),
					new PropertyUri(PropertyUriEnum.ConversationInstanceKey),
					new PropertyUri(PropertyUriEnum.ConversationIconIndex),
					new PropertyUri(PropertyUriEnum.ConversationGlobalIconIndex),
					new PropertyUri(PropertyUriEnum.ConversationHasIrm),
					new PropertyUri(PropertyUriEnum.ConversationUniqueSenders)
				};
				foreach (PropertyUriEnum uri in ConversationFeedLoader.ConversationFeedProperties)
				{
					list.Add(new PropertyUri(uri));
				}
				conversationResponseShape.AdditionalProperties = list.ToArray();
				return conversationResponseShape;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x00081DB4 File Offset: 0x0007FFB4
		private static ItemResponseShape EditableItems
		{
			get
			{
				return WellKnownShapes.CreateExpandedShape<ItemResponseShape>(WellKnownShapes.ItemPartNormalizedBody, new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.BccRecipients),
					new PropertyUri(PropertyUriEnum.Body)
				});
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x00081DE7 File Offset: 0x0007FFE7
		private static ItemResponseShape MailCompose
		{
			get
			{
				return WellKnownShapes.CreateComposeResponseShape(false);
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002424 RID: 9252 RVA: 0x00081DF0 File Offset: 0x0007FFF0
		private static ItemResponseShape MailListItem
		{
			get
			{
				return new ItemResponseShape
				{
					BaseShape = ShapeEnum.IdOnly,
					AdditionalProperties = new PropertyPath[]
					{
						new PropertyUri(PropertyUriEnum.ItemId),
						new PropertyUri(PropertyUriEnum.ItemParentId),
						new PropertyUri(PropertyUriEnum.ConversationId),
						new PropertyUri(PropertyUriEnum.Subject),
						WellKnownProperties.NormalizedSubject,
						new PropertyUri(PropertyUriEnum.Importance),
						new PropertyUri(PropertyUriEnum.Sensitivity),
						new PropertyUri(PropertyUriEnum.DateTimeReceived),
						new PropertyUri(PropertyUriEnum.DateTimeCreated),
						new PropertyUri(PropertyUriEnum.DateTimeSent),
						new PropertyUri(PropertyUriEnum.ItemLastModifiedTime),
						new PropertyUri(PropertyUriEnum.HasAttachments),
						new PropertyUri(PropertyUriEnum.IsDraft),
						new PropertyUri(PropertyUriEnum.ItemClass),
						new PropertyUri(PropertyUriEnum.From),
						new PropertyUri(PropertyUriEnum.Sender),
						new PropertyUri(PropertyUriEnum.InstanceKey),
						new PropertyUri(PropertyUriEnum.Size),
						new PropertyUri(PropertyUriEnum.IsRead),
						new PropertyUri(PropertyUriEnum.Flag),
						new PropertyUri(PropertyUriEnum.Preview),
						new PropertyUri(PropertyUriEnum.ItemPolicyTag),
						new PropertyUri(PropertyUriEnum.ItemArchiveTag),
						WellKnownProperties.RetentionPeriod,
						WellKnownProperties.ArchivePeriod,
						WellKnownProperties.SharingInstanceGuid,
						new PropertyUri(PropertyUriEnum.DisplayTo),
						new PropertyUri(PropertyUriEnum.Categories),
						new PropertyUri(PropertyUriEnum.IsDeliveryReceiptRequested),
						new PropertyUri(PropertyUriEnum.IsReadReceiptRequested),
						new PropertyUri(PropertyUriEnum.TaskStatus),
						new PropertyUri(PropertyUriEnum.IconIndex),
						WellKnownProperties.LastVerbExecuted,
						WellKnownProperties.LastVerbExecutionTime
					},
					FlightedProperties = new Dictionary<string, PropertyPath[]>
					{
						{
							"SuperSort",
							new PropertyPath[]
							{
								new PropertyUri(PropertyUriEnum.ReceivedOrRenewTime)
							}
						}
					}
				};
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x00081FA8 File Offset: 0x000801A8
		private static ItemResponseShape TaskListItem
		{
			get
			{
				return new ItemResponseShape
				{
					BaseShape = ShapeEnum.IdOnly,
					AdditionalProperties = new PropertyPath[]
					{
						new PropertyUri(PropertyUriEnum.ItemId),
						new PropertyUri(PropertyUriEnum.ItemParentId),
						new PropertyUri(PropertyUriEnum.InstanceKey),
						new PropertyUri(PropertyUriEnum.Subject),
						new PropertyUri(PropertyUriEnum.Importance),
						new PropertyUri(PropertyUriEnum.Sensitivity),
						new PropertyUri(PropertyUriEnum.HasAttachments),
						new PropertyUri(PropertyUriEnum.ItemClass),
						new PropertyUri(PropertyUriEnum.IsRead),
						new PropertyUri(PropertyUriEnum.Flag),
						new PropertyUri(PropertyUriEnum.TaskDoItTime),
						new PropertyUri(PropertyUriEnum.TaskDueDate),
						new PropertyUri(PropertyUriEnum.TaskIsComplete),
						new PropertyUri(PropertyUriEnum.TaskIsTaskRecurring),
						new PropertyUri(PropertyUriEnum.TaskStatus),
						new PropertyUri(PropertyUriEnum.TaskCompleteDate),
						new PropertyUri(PropertyUriEnum.TaskStartDate),
						new PropertyUri(PropertyUriEnum.TaskDelegationState),
						new PropertyUri(PropertyUriEnum.Categories)
					}
				};
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002426 RID: 9254 RVA: 0x000820B4 File Offset: 0x000802B4
		private static ItemResponseShape MessageDetails
		{
			get
			{
				return new ItemResponseShape
				{
					BaseShape = ShapeEnum.IdOnly,
					AdditionalProperties = new PropertyPath[]
					{
						WellKnownProperties.InternetMessageHeaders
					}
				};
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002427 RID: 9255 RVA: 0x000820E8 File Offset: 0x000802E8
		private static PropertyPath[] CommonMailReadingPaneShape
		{
			get
			{
				List<PropertyPath> list = new List<PropertyPath>(WellKnownShapes.CommonMailReadingPaneEwsShape);
				list.AddRange(new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.ModernReminders),
					new PropertyUri(PropertyUriEnum.RecipientCounts),
					new PropertyUri(PropertyUriEnum.AttendeeCounts)
				});
				return list.ToArray();
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x00082138 File Offset: 0x00080338
		private static PropertyPath[] CommonMailReadingPaneEwsShape
		{
			get
			{
				List<PropertyPath> list = new List<PropertyPath>(WellKnownShapes.MailListItem.AdditionalProperties);
				list.AddRange(new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.Attachments),
					new PropertyUri(PropertyUriEnum.ToRecipients),
					new PropertyUri(PropertyUriEnum.CcRecipients),
					new PropertyUri(PropertyUriEnum.BccRecipients),
					new PropertyUri(PropertyUriEnum.ResponseObjects),
					new PropertyUri(PropertyUriEnum.Start),
					new PropertyUri(PropertyUriEnum.StartWallClock),
					new PropertyUri(PropertyUriEnum.StartTimeZoneId),
					new PropertyUri(PropertyUriEnum.End),
					new PropertyUri(PropertyUriEnum.EndWallClock),
					new PropertyUri(PropertyUriEnum.EndTimeZoneId),
					WellKnownProperties.Location,
					new PropertyUri(PropertyUriEnum.MeetingRequestType),
					new PropertyUri(PropertyUriEnum.ChangeHighlights),
					new PropertyUri(PropertyUriEnum.Recurrence),
					new PropertyUri(PropertyUriEnum.RecurrenceId),
					new PropertyUri(PropertyUriEnum.ResponseType),
					new PropertyUri(PropertyUriEnum.IsResponseRequested),
					new PropertyUri(PropertyUriEnum.IsCancelled),
					new PropertyUri(PropertyUriEnum.AssociatedCalendarItemId),
					new PropertyUri(PropertyUriEnum.IsOutOfDate),
					new PropertyUri(PropertyUriEnum.IsDelegated),
					new PropertyUri(PropertyUriEnum.IsRecurring),
					new PropertyUri(PropertyUriEnum.IntendedFreeBusyStatus),
					WellKnownProperties.VoiceMessageAttachmentOrder,
					WellKnownProperties.PstnCallbackTelephoneNumber,
					WellKnownProperties.VoiceMessageDuration,
					new PropertyUri(PropertyUriEnum.EntityExtractionResult),
					new PropertyUri(PropertyUriEnum.RequiredAttendees),
					new PropertyUri(PropertyUriEnum.OptionalAttendees),
					new PropertyUri(PropertyUriEnum.CalendarItemType),
					new PropertyUri(PropertyUriEnum.InternetMessageId),
					new PropertyUri(PropertyUriEnum.Organizer),
					WellKnownProperties.IsClassified,
					WellKnownProperties.ClassificationGuid,
					WellKnownProperties.Classification,
					WellKnownProperties.ClassificationDescription,
					WellKnownProperties.ClassificationKeep,
					new PropertyUri(PropertyUriEnum.RightsManagementLicenseData),
					new PropertyUri(PropertyUriEnum.ItemRetentionDate),
					new PropertyUri(PropertyUriEnum.BlockStatus),
					new PropertyUri(PropertyUriEnum.HasBlockedImages),
					WellKnownProperties.MessageBccMe,
					new PropertyUri(PropertyUriEnum.ReplyTo),
					new PropertyUri(PropertyUriEnum.ProposedStart),
					new PropertyUri(PropertyUriEnum.ProposedEnd),
					WellKnownProperties.NativeBodyInfo,
					new PropertyUri(PropertyUriEnum.IsOrganizer),
					new PropertyUri(PropertyUriEnum.ReceivedRepresenting),
					new PropertyUri(PropertyUriEnum.ApprovalRequestData),
					new PropertyUri(PropertyUriEnum.VotingInformation),
					new PropertyUri(PropertyUriEnum.IsClutter),
					new PropertyUri(PropertyUriEnum.ReminderMessageData),
					WellKnownProperties.DocumentId
				});
				return list.ToArray();
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002429 RID: 9257 RVA: 0x000823B3 File Offset: 0x000805B3
		private static ItemResponseShape FullCalendarItem
		{
			get
			{
				return WellKnownShapes.CreateFullCalendarItemResponseShape();
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x000823BA File Offset: 0x000805BA
		private static ItemResponseShape MailComposeNormalizedBody
		{
			get
			{
				return WellKnownShapes.CreateComposeResponseShape(true);
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x0600242B RID: 9259 RVA: 0x000823C2 File Offset: 0x000805C2
		private static ItemResponseShape QuickComposeItemPart
		{
			get
			{
				return WellKnownShapes.CreateExpandedShapeFromProperties<ItemResponseShape>(WellKnownShapes.CommonMailReadingPaneShape, new PropertyPath[0]);
			}
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000823D4 File Offset: 0x000805D4
		internal static void SetDefaultsOnItemResponseShape(ItemResponseShape shape, LayoutType layout, OwaUserConfiguration owaUserConfiguration = null)
		{
			bool flag = owaUserConfiguration == null;
			if (owaUserConfiguration != null && owaUserConfiguration.ApplicationSettings.FilterWebBeaconsAndHtmlForms == WebBeaconFilterLevels.DisableFilter)
			{
				shape.FilterHtmlContent = false;
				shape.BlockExternalImagesIfSenderUntrusted = false;
			}
			else
			{
				shape.FilterHtmlContent = true;
				if (flag)
				{
					shape.BlockExternalImages = true;
				}
				else
				{
					shape.BlockExternalImagesIfSenderUntrusted = true;
				}
			}
			if (owaUserConfiguration != null && owaUserConfiguration.SegmentationSettings.PredictedActions)
			{
				shape.InferenceEnabled = true;
			}
			shape.AddBlankTargetToLinks = true;
			shape.ClientSupportsIrm = !flag;
			shape.MaximumBodySize = ((layout == LayoutType.Mouse) ? 2097152 : 51200);
			shape.MaximumRecipientsToReturn = ((layout == LayoutType.Mouse) ? 10 : 0);
			if (!flag)
			{
				shape.InlineImageUrlTemplate = "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAEALAAAAAABAAEAAAIBTAA7";
				shape.InlineImageUrlOnLoadTemplate = "InlineImageLoader.GetLoader().Load(this)";
				shape.InlineImageCustomDataTemplate = "{id}";
			}
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x00082494 File Offset: 0x00080694
		private static T CreateExpandedShape<T>(T baseShape, PropertyPath[] expandedProperties) where T : ResponseShape, new()
		{
			T result = WellKnownShapes.CreateExpandedShapeFromProperties<T>(baseShape.AdditionalProperties, expandedProperties);
			result.FlightedProperties = new Dictionary<string, PropertyPath[]>(baseShape.FlightedProperties);
			return result;
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000824D8 File Offset: 0x000806D8
		private static T CreateExpandedShapeFromProperties<T>(PropertyPath[] propertiesToClone, PropertyPath[] additionalProperties) where T : ResponseShape, new()
		{
			T result = Activator.CreateInstance<T>();
			result.BaseShape = ShapeEnum.IdOnly;
			List<PropertyPath> list = new List<PropertyPath>(propertiesToClone);
			list.AddRange(additionalProperties);
			result.AdditionalProperties = list.ToArray();
			return result;
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x0008251C File Offset: 0x0008071C
		private static ItemResponseShape CreateComposeResponseShape(bool isNormalized)
		{
			return new ItemResponseShape
			{
				BaseShape = ShapeEnum.IdOnly,
				AdditionalProperties = new PropertyPath[]
				{
					new PropertyUri(PropertyUriEnum.ItemClass),
					new PropertyUri(PropertyUriEnum.ItemParentId),
					new PropertyUri(PropertyUriEnum.ToRecipients),
					new PropertyUri(PropertyUriEnum.CcRecipients),
					new PropertyUri(PropertyUriEnum.BccRecipients),
					new PropertyUri(PropertyUriEnum.From),
					new PropertyUri(PropertyUriEnum.Sender),
					new PropertyUri(PropertyUriEnum.ReplyTo),
					isNormalized ? new PropertyUri(PropertyUriEnum.NormalizedBody) : new PropertyUri(PropertyUriEnum.Body),
					new PropertyUri(PropertyUriEnum.Subject),
					WellKnownProperties.NormalizedSubject,
					new PropertyUri(PropertyUriEnum.Importance),
					new PropertyUri(PropertyUriEnum.Attachments),
					new PropertyUri(PropertyUriEnum.Start),
					new PropertyUri(PropertyUriEnum.End),
					new PropertyUri(PropertyUriEnum.ResponseType),
					new PropertyUri(PropertyUriEnum.Recurrence),
					new PropertyUri(PropertyUriEnum.IsDraft),
					new PropertyUri(PropertyUriEnum.ConversationId),
					WellKnownProperties.IsClassified,
					WellKnownProperties.ClassificationGuid,
					WellKnownProperties.Classification,
					WellKnownProperties.ClassificationDescription,
					WellKnownProperties.ClassificationKeep,
					new PropertyUri(PropertyUriEnum.RightsManagementLicenseData),
					new PropertyUri(PropertyUriEnum.Sensitivity),
					new PropertyUri(PropertyUriEnum.IsDeliveryReceiptRequested),
					new PropertyUri(PropertyUriEnum.IsReadReceiptRequested),
					WellKnownProperties.NativeBodyInfo,
					WellKnownProperties.Location,
					new PropertyUri(PropertyUriEnum.IsGroupEscalationMessage)
				},
				ShouldUseNarrowGapForPTagHtmlToTextConversion = true
			};
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x00082698 File Offset: 0x00080898
		private static AttachmentResponseShape CreateItemAttachmentBaseResponseShape()
		{
			AttachmentResponseShape attachmentResponseShape = new AttachmentResponseShape
			{
				BaseShape = ShapeEnum.IdOnly
			};
			List<PropertyPath> list = new List<PropertyPath>();
			list.AddRange(WellKnownShapes.GetItemAttachmentAdditionalProperties());
			list.AddRange(WellKnownShapes.GetFullCalendarItemAdditionalProperties());
			attachmentResponseShape.AdditionalProperties = list.ToArray();
			return attachmentResponseShape;
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000826E0 File Offset: 0x000808E0
		private static PropertyPath[] GetItemAttachmentAdditionalProperties()
		{
			return new PropertyPath[]
			{
				new PropertyUri(PropertyUriEnum.InstanceKey),
				new PropertyUri(PropertyUriEnum.NormalizedBody),
				new PropertyUri(PropertyUriEnum.ItemParentId),
				new PropertyUri(PropertyUriEnum.Attachments),
				new PropertyUri(PropertyUriEnum.Subject),
				new PropertyUri(PropertyUriEnum.Importance),
				new PropertyUri(PropertyUriEnum.Sensitivity),
				new PropertyUri(PropertyUriEnum.DateTimeCreated),
				new PropertyUri(PropertyUriEnum.DateTimeReceived),
				new PropertyUri(PropertyUriEnum.HasAttachments),
				new PropertyUri(PropertyUriEnum.IsDraft),
				new PropertyUri(PropertyUriEnum.ItemClass),
				new PropertyUri(PropertyUriEnum.From),
				new PropertyUri(PropertyUriEnum.Sender),
				new PropertyUri(PropertyUriEnum.ToRecipients),
				new PropertyUri(PropertyUriEnum.CcRecipients),
				new PropertyUri(PropertyUriEnum.BccRecipients),
				new PropertyUri(PropertyUriEnum.DisplayTo),
				new PropertyUri(PropertyUriEnum.IsRead),
				new PropertyUri(PropertyUriEnum.ConversationId),
				new PropertyUri(PropertyUriEnum.ConversationIndex),
				new PropertyUri(PropertyUriEnum.ResponseObjects),
				new PropertyUri(PropertyUriEnum.Start),
				new PropertyUri(PropertyUriEnum.StartWallClock),
				new PropertyUri(PropertyUriEnum.StartTimeZoneId),
				new PropertyUri(PropertyUriEnum.End),
				new PropertyUri(PropertyUriEnum.EndWallClock),
				new PropertyUri(PropertyUriEnum.EndTimeZoneId),
				new PropertyUri(PropertyUriEnum.MeetingRequestType),
				new PropertyUri(PropertyUriEnum.ChangeHighlights),
				new PropertyUri(PropertyUriEnum.Recurrence),
				new PropertyUri(PropertyUriEnum.RecurrenceId),
				new PropertyUri(PropertyUriEnum.ResponseType),
				new PropertyUri(PropertyUriEnum.IsResponseRequested),
				new PropertyUri(PropertyUriEnum.AssociatedCalendarItemId),
				new PropertyUri(PropertyUriEnum.IsOutOfDate),
				new PropertyUri(PropertyUriEnum.IsDelegated),
				new PropertyUri(PropertyUriEnum.IsRecurring),
				new PropertyUri(PropertyUriEnum.IntendedFreeBusyStatus),
				new PropertyUri(PropertyUriEnum.IsClutter),
				new PropertyUri(PropertyUriEnum.Preview),
				WellKnownProperties.VoiceMessageAttachmentOrder,
				WellKnownProperties.PstnCallbackTelephoneNumber,
				WellKnownProperties.VoiceMessageDuration,
				new PropertyUri(PropertyUriEnum.Flag),
				WellKnownProperties.NormalizedSubject,
				new PropertyUri(PropertyUriEnum.EntityExtractionResult),
				new PropertyUri(PropertyUriEnum.ItemLastModifiedTime),
				new PropertyUri(PropertyUriEnum.RequiredAttendees),
				new PropertyUri(PropertyUriEnum.OptionalAttendees),
				new PropertyUri(PropertyUriEnum.CalendarItemType),
				new PropertyUri(PropertyUriEnum.InternetMessageId),
				new PropertyUri(PropertyUriEnum.Organizer),
				WellKnownProperties.IsClassified,
				WellKnownProperties.ClassificationGuid,
				WellKnownProperties.Classification,
				WellKnownProperties.ClassificationDescription,
				WellKnownProperties.ClassificationKeep,
				WellKnownProperties.SharingInstanceGuid,
				new PropertyUri(PropertyUriEnum.RightsManagementLicenseData),
				new PropertyUri(PropertyUriEnum.Categories),
				new PropertyUri(PropertyUriEnum.BlockStatus),
				new PropertyUri(PropertyUriEnum.HasBlockedImages),
				WellKnownProperties.MessageBccMe,
				new PropertyUri(PropertyUriEnum.ReplyTo),
				new PropertyUri(PropertyUriEnum.ProposedStart),
				new PropertyUri(PropertyUriEnum.ProposedEnd),
				WellKnownProperties.NativeBodyInfo,
				WellKnownProperties.Location,
				new PropertyUri(PropertyUriEnum.IconIndex),
				new PropertyUri(PropertyUriEnum.TaskDueDate),
				new PropertyUri(PropertyUriEnum.TaskStartDate),
				new PropertyUri(PropertyUriEnum.TaskStatus),
				new PropertyUri(PropertyUriEnum.TaskCompleteDate),
				new PropertyUri(PropertyUriEnum.TaskPercentComplete),
				new PropertyUri(PropertyUriEnum.TaskOwner),
				new PropertyUri(PropertyUriEnum.TaskTotalWork),
				new PropertyUri(PropertyUriEnum.TaskActualWork),
				new PropertyUri(PropertyUriEnum.TaskMileage),
				new PropertyUri(PropertyUriEnum.TaskBillingInformation),
				new PropertyUri(PropertyUriEnum.TaskCompanies),
				new PropertyUri(PropertyUriEnum.TaskRecurrence),
				new PropertyUri(PropertyUriEnum.TaskIsComplete),
				new PropertyUri(PropertyUriEnum.TaskIsTaskRecurring),
				new PropertyUri(PropertyUriEnum.ReminderIsSet),
				new PropertyUri(PropertyUriEnum.ReminderDueBy)
			};
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x00082AC8 File Offset: 0x00080CC8
		private static PropertyPath[] GetFullCalendarItemAdditionalProperties()
		{
			return new PropertyPath[]
			{
				new PropertyUri(PropertyUriEnum.UID),
				new PropertyUri(PropertyUriEnum.ItemParentId),
				new PropertyUri(PropertyUriEnum.Sensitivity),
				new PropertyUri(PropertyUriEnum.IsCancelled),
				new PropertyUri(PropertyUriEnum.IsSeriesCancelled),
				new PropertyUri(PropertyUriEnum.AppointmentState),
				new PropertyUri(PropertyUriEnum.LegacyFreeBusyStatus),
				new PropertyUri(PropertyUriEnum.IntendedFreeBusyStatus),
				new PropertyUri(PropertyUriEnum.CalendarItemType),
				new PropertyUri(PropertyUriEnum.Start),
				new PropertyUri(PropertyUriEnum.StartWallClock),
				new PropertyUri(PropertyUriEnum.StartTimeZoneId),
				new PropertyUri(PropertyUriEnum.End),
				new PropertyUri(PropertyUriEnum.EndWallClock),
				new PropertyUri(PropertyUriEnum.EndTimeZoneId),
				new PropertyUri(PropertyUriEnum.MyResponseType),
				new PropertyUri(PropertyUriEnum.IsAllDayEvent),
				new PropertyUri(PropertyUriEnum.Organizer),
				new PropertyUri(PropertyUriEnum.RequiredAttendees),
				new PropertyUri(PropertyUriEnum.OptionalAttendees),
				new PropertyUri(PropertyUriEnum.Resources),
				new PropertyUri(PropertyUriEnum.AppointmentReplyTime),
				new PropertyUri(PropertyUriEnum.AppointmentReplyName),
				new PropertyUri(PropertyUriEnum.Subject),
				new PropertyUri(PropertyUriEnum.HasAttachments),
				new PropertyUri(PropertyUriEnum.Attachments),
				new PropertyUri(PropertyUriEnum.Body),
				new PropertyUri(PropertyUriEnum.BlockStatus),
				new PropertyUri(PropertyUriEnum.HasBlockedImages),
				new PropertyUri(PropertyUriEnum.DateTimeReceived),
				new PropertyUri(PropertyUriEnum.Recurrence),
				new PropertyUri(PropertyUriEnum.ReminderIsSet),
				new PropertyUri(PropertyUriEnum.ReminderMinutesBeforeStart),
				new PropertyUri(PropertyUriEnum.CalendarIsResponseRequested),
				new PropertyUri(PropertyUriEnum.ItemClass),
				new PropertyUri(PropertyUriEnum.ItemLastModifiedTime),
				new PropertyUri(PropertyUriEnum.IsMeeting),
				new PropertyUri(PropertyUriEnum.DateTimeCreated),
				new PropertyUri(PropertyUriEnum.EntityExtractionResult),
				new PropertyUri(PropertyUriEnum.InstanceKey),
				new PropertyUri(PropertyUriEnum.ItemEffectiveRights),
				new PropertyUri(PropertyUriEnum.Categories),
				new PropertyUri(PropertyUriEnum.JoinOnlineMeetingUrl),
				new PropertyUri(PropertyUriEnum.OnlineMeetingSettings),
				new PropertyUri(PropertyUriEnum.ConversationId),
				new PropertyUri(PropertyUriEnum.IsRecurring),
				new PropertyUri(PropertyUriEnum.IsOrganizer),
				new PropertyUri(PropertyUriEnum.Preview),
				new PropertyUri(PropertyUriEnum.InboxReminders),
				WellKnownProperties.NormalizedSubject,
				WellKnownProperties.NativeBodyInfo,
				WellKnownProperties.Location
			};
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x00082D3C File Offset: 0x00080F3C
		private static ItemResponseShape CreateFullCalendarItemResponseShape()
		{
			return new ItemResponseShape
			{
				BaseShape = ShapeEnum.IdOnly,
				AddBlankTargetToLinks = true,
				FilterHtmlContent = true,
				InlineImageUrlTemplate = "/service.svc/s/GetFileAttachment",
				AdditionalProperties = WellKnownShapes.GetFullCalendarItemAdditionalProperties()
			};
		}

		// Token: 0x040013AD RID: 5037
		public const int MaximumBodySizeForMouseLayout = 2097152;

		// Token: 0x040013AE RID: 5038
		public const int MaximumBodySizeForTouchLayout = 51200;

		// Token: 0x040013AF RID: 5039
		public const int MaximumRecipientsToReturnForMouseLayout = 10;

		// Token: 0x040013B0 RID: 5040
		public const int NoRecipientsTruncation = 0;

		// Token: 0x040013B1 RID: 5041
		public const int MaxInitialItemPartsPerConversation = 20;

		// Token: 0x040013B2 RID: 5042
		public const string TypeName = "InlineImageLoader";

		// Token: 0x040013B3 RID: 5043
		private const string PlaceHolderUri = "data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAEALAAAAAABAAEAAAIBTAA7";

		// Token: 0x040013B4 RID: 5044
		private const string OnLoadTemplate = "InlineImageLoader.GetLoader().Load(this)";

		// Token: 0x040013B5 RID: 5045
		private const string CustomDataTemplate = "{id}";

		// Token: 0x040013B6 RID: 5046
		private const string CssScopeNamePrefix = "rps_";

		// Token: 0x040013B7 RID: 5047
		private static LazyMember<Dictionary<WellKnownShapeName, ResponseShape>> responseShapes = new LazyMember<Dictionary<WellKnownShapeName, ResponseShape>>(() => new Dictionary<WellKnownShapeName, ResponseShape>
		{
			{
				WellKnownShapeName.ConversationListView,
				WellKnownShapes.FindConversationNormalShape
			},
			{
				WellKnownShapeName.ConversationSentItemsListView,
				WellKnownShapes.FindConversationSentItemsShape
			},
			{
				WellKnownShapeName.ConversationUberListView,
				WellKnownShapes.FindConversationUberShape
			},
			{
				WellKnownShapeName.ItemNormalizedBody,
				WellKnownShapes.ItemNormalizedBody
			},
			{
				WellKnownShapeName.EditableItems,
				WellKnownShapes.EditableItems
			},
			{
				WellKnownShapeName.Folder,
				WellKnownShapes.Folder
			},
			{
				WellKnownShapeName.ItemAttachment,
				WellKnownShapes.ItemAttachment
			},
			{
				WellKnownShapeName.ItemPartNormalizedBody,
				WellKnownShapes.ItemPartNormalizedBody
			},
			{
				WellKnownShapeName.ItemPartUniqueBody,
				WellKnownShapes.ItemPartUniqueBody
			},
			{
				WellKnownShapeName.MailCompose,
				WellKnownShapes.MailCompose
			},
			{
				WellKnownShapeName.MailListItem,
				WellKnownShapes.MailListItem
			},
			{
				WellKnownShapeName.MessageDetails,
				WellKnownShapes.MessageDetails
			},
			{
				WellKnownShapeName.TaskListItem,
				WellKnownShapes.TaskListItem
			},
			{
				WellKnownShapeName.FullCalendarItem,
				WellKnownShapes.FullCalendarItem
			},
			{
				WellKnownShapeName.MailComposeNormalizedBody,
				WellKnownShapes.MailComposeNormalizedBody
			},
			{
				WellKnownShapeName.QuickComposeItemPart,
				WellKnownShapes.QuickComposeItemPart
			},
			{
				WellKnownShapeName.GroupConversationListView,
				WellKnownShapes.GroupConversationListView
			},
			{
				WellKnownShapeName.GroupConversationFeedView,
				WellKnownShapes.GroupConversationFeedView
			},
			{
				WellKnownShapeName.InferenceConversationListView,
				WellKnownShapes.InferenceFindConversationNormalShape
			},
			{
				WellKnownShapeName.InferenceConversationUberListView,
				WellKnownShapes.InferenceFindConversationUberShape
			},
			{
				WellKnownShapeName.DiscoveryItem,
				WellKnownShapes.DiscoveryItemShape
			}
		});

		// Token: 0x040013B8 RID: 5048
		private static readonly List<DistinguishedFolderIdName> requiredDistinguishedFolders = new List<DistinguishedFolderIdName>
		{
			DistinguishedFolderIdName.deleteditems,
			DistinguishedFolderIdName.drafts,
			DistinguishedFolderIdName.inbox,
			DistinguishedFolderIdName.junkemail,
			DistinguishedFolderIdName.notes,
			DistinguishedFolderIdName.sentitems
		};

		// Token: 0x040013B9 RID: 5049
		private static readonly List<DistinguishedFolderIdName> foldersToMoveToTop = new List<DistinguishedFolderIdName>
		{
			DistinguishedFolderIdName.inbox,
			DistinguishedFolderIdName.drafts,
			DistinguishedFolderIdName.sentitems,
			DistinguishedFolderIdName.deleteditems
		};
	}
}
