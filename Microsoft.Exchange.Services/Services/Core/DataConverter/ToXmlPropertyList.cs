using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001BD RID: 445
	internal class ToXmlPropertyList : ToXmlPropertyListBase
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x0003D538 File Offset: 0x0003B738
		private ToXmlPropertyList()
		{
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0003D540 File Offset: 0x0003B740
		public ToXmlPropertyList(Shape shape, ResponseShape responseShape) : base(shape, responseShape)
		{
		}

		// Token: 0x1700017B RID: 379
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0003D54A File Offset: 0x0003B74A
		public char[] CharBuffer
		{
			set
			{
				this.charBuffer = value;
			}
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0003D553 File Offset: 0x0003B753
		protected override ToXmlCommandSettingsBase GetCommandSettings()
		{
			return new ToXmlCommandSettings();
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0003D55A File Offset: 0x0003B75A
		protected override ToXmlCommandSettingsBase GetCommandSettings(PropertyPath propertyPath)
		{
			return new ToXmlCommandSettings(propertyPath);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0003D564 File Offset: 0x0003B764
		protected override bool ValidateProperty(PropertyInformation propertyInformation, bool returnErrorForInvalidProperty)
		{
			bool implementsToXmlCommand = propertyInformation.ImplementsToXmlCommand;
			if (!implementsToXmlCommand && returnErrorForInvalidProperty)
			{
				throw new InvalidPropertyForOperationException(propertyInformation.PropertyPath);
			}
			return implementsToXmlCommand;
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0003D58D File Offset: 0x0003B78D
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x0003D595 File Offset: 0x0003B795
		internal bool IgnoreCorruptPropertiesWhenRendering
		{
			get
			{
				return this.ignoreCorruptPropertiesWhenRendering;
			}
			set
			{
				this.ignoreCorruptPropertiesWhenRendering = value;
			}
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0003D5A0 File Offset: 0x0003B7A0
		private void CheckAndAddParticipantToConvert(List<Participant> participantsToConvert, Participant participantToCheckAndAdd)
		{
			bool flag = false;
			for (int i = 0; i < participantsToConvert.Count; i++)
			{
				if (participantsToConvert[i] == participantToCheckAndAdd)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				ExTraceGlobals.ParticipantLookupBatchingTracer.TraceDebug<string, int>((long)this.GetHashCode(), "ToXmlPropertyList.CheckAndAddParticipantToConvert - converting participant with EmailAddress = {0} and HashCode = {1}.", participantToCheckAndAdd.EmailAddress, participantToCheckAndAdd.GetHashCode());
				participantsToConvert.Add(participantToCheckAndAdd);
			}
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0003D600 File Offset: 0x0003B800
		private void ConvertAndGetParticipantInformation(List<Participant> participants)
		{
			if (participants.Count == 0)
			{
				return;
			}
			List<Participant> list = new List<Participant>(participants.Count);
			ParticipantInformationDictionary participantInformation = EWSSettings.ParticipantInformation;
			foreach (Participant participant in participants)
			{
				ParticipantInformation participantInformation2 = null;
				if (participant == null)
				{
					ExTraceGlobals.ParticipantLookupBatchingTracer.TraceDebug((long)this.GetHashCode(), "ToXmlPropertyList.ConvertAndGetParticipantInformation - found null entry");
				}
				else if (participantInformation.ContainsParticipant(participant))
				{
					ExTraceGlobals.ParticipantLookupBatchingTracer.TraceDebug<string, int>((long)this.GetHashCode(), "ToXmlPropertyList.ConvertAndGetParticipantInformation - using already resolved participant for EmailAddress = {0}, HashCode = {1}", participant.EmailAddress, participant.GetHashCode());
				}
				else
				{
					if (string.Equals(participant.RoutingType, "EX", StringComparison.OrdinalIgnoreCase))
					{
						this.CheckAndAddParticipantToConvert(list, participant);
					}
					else
					{
						string valueOrDefault = participant.GetValueOrDefault<string>(ParticipantSchema.SipUri, null);
						participantInformation2 = new ParticipantInformation(participant.DisplayName, participant.RoutingType, participant.EmailAddress, participant.Origin, null, valueOrDefault, new bool?(participant.Submitted));
					}
					if (participantInformation2 != null)
					{
						participantInformation.AddParticipant(participant, participantInformation2);
					}
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			ExTraceGlobals.ParticipantLookupBatchingTracer.TraceDebug<int>((long)this.GetHashCode(), "ToXmlPropertyList.ConvertAndGetParticipantInformation - now calling TryConvertTo on {0} participants", list.Count);
			Participant[] array = MailboxHelper.TryConvertTo(list.ToArray(), "SMTP");
			int num = 0;
			foreach (Participant participant2 in array)
			{
				Participant participant3;
				bool value;
				if (participant2 == null || participant2.EmailAddress == null)
				{
					participant3 = list[num];
					value = true;
				}
				else
				{
					participant3 = participant2;
					value = false;
				}
				string valueOrDefault2 = participant3.GetValueOrDefault<string>(ParticipantSchema.SipUri, null);
				ParticipantInformation participantInformation3 = new ParticipantInformation(participant3.DisplayName, participant3.RoutingType, participant3.EmailAddress, participant3.Origin, new bool?(value), valueOrDefault2, new bool?(participant3.Submitted));
				participantInformation.AddParticipant(list[num], participantInformation3);
				num++;
			}
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0003D814 File Offset: 0x0003BA14
		public IList<IToXmlCommand> CreatePropertyCommands(IdAndSession idAndSession, StoreObject storeObject, XmlElement serviceItem)
		{
			List<IToXmlCommand> list = new List<IToXmlCommand>();
			foreach (CommandContext commandContext in this.commandContextsOrdered)
			{
				ToXmlCommandSettings toXmlCommandSettings = (ToXmlCommandSettings)commandContext.CommandSettings;
				toXmlCommandSettings.IdAndSession = idAndSession;
				toXmlCommandSettings.StoreObject = storeObject;
				toXmlCommandSettings.ServiceItem = serviceItem;
				toXmlCommandSettings.ResponseShape = this.responseShape;
				list.Add((IToXmlCommand)commandContext.PropertyInformation.CreatePropertyCommand(commandContext));
			}
			return list;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0003D8B0 File Offset: 0x0003BAB0
		private void ConvertStoreObjectPropertiesToXml(XmlElement serviceItem, IdAndSession idAndSession, StoreObject storeObject)
		{
			IList<IToXmlCommand> list = this.CreatePropertyCommands(idAndSession, storeObject, serviceItem);
			List<Participant> participants = new List<Participant>();
			foreach (IToXmlCommand toXmlCommand in list)
			{
				IPregatherParticipants pregatherParticipants = toXmlCommand as IPregatherParticipants;
				if (pregatherParticipants != null)
				{
					pregatherParticipants.Pregather(storeObject, participants);
				}
			}
			this.ConvertAndGetParticipantInformation(participants);
			foreach (IToXmlCommand toXmlCommand2 in list)
			{
				IRequireCharBuffer requireCharBuffer = toXmlCommand2 as IRequireCharBuffer;
				if (requireCharBuffer != null)
				{
					requireCharBuffer.CharBuffer = this.charBuffer;
				}
				try
				{
					toXmlCommand2.ToXml();
				}
				catch (PropertyRequestFailedException ex)
				{
					if (!this.IgnoreCorruptPropertiesWhenRendering)
					{
						throw;
					}
					if (ExTraceGlobals.ServiceCommandBaseDataTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.ServiceCommandBaseDataTracer.TraceError<string, string>((long)this.GetHashCode(), "[ToXmlPropertyList::ConvertStoreObjectPropertiesToXml] Encountered PropertyRequestFailedException.  Message: '{0}'. Data: {1} IgnoreCorruptPropertiesWhenRendering is true, so processing will continue.", ex.Message, ((IProvidePropertyPaths)ex).GetPropertyPathsString());
					}
				}
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0003D9C4 File Offset: 0x0003BBC4
		public XmlElement ConvertStoreObjectPropertiesToXml(IdAndSession idAndSession, StoreObject storeObject, XmlDocument parentDocument)
		{
			XmlElement xmlElement = base.CreateItemXmlElement(parentDocument);
			this.ConvertStoreObjectPropertiesToXml(xmlElement, idAndSession, storeObject);
			return xmlElement;
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0003D9E4 File Offset: 0x0003BBE4
		public XmlElement ConvertStoreObjectPropertiesToXml(IdAndSession idAndSession, StoreObject storeObject, XmlElement parentElement)
		{
			XmlElement xmlElement = base.CreateItemXmlElement(parentElement);
			this.ConvertStoreObjectPropertiesToXml(xmlElement, idAndSession, storeObject);
			return xmlElement;
		}

		// Token: 0x04000978 RID: 2424
		private bool ignoreCorruptPropertiesWhenRendering;

		// Token: 0x04000979 RID: 2425
		public static ToXmlPropertyList Empty = new ToXmlPropertyList();

		// Token: 0x0400097A RID: 2426
		private char[] charBuffer;
	}
}
