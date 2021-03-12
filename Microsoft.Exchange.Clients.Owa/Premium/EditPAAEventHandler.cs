using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.UM.PersonalAutoAttendant;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004AE RID: 1198
	[OwaEventNamespace("PAA")]
	internal sealed class EditPAAEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002DF1 RID: 11761 RVA: 0x00104BBC File Offset: 0x00102DBC
		public static void Register()
		{
			OwaEventRegistry.RegisterStruct(typeof(PAADurationInfo));
			OwaEventRegistry.RegisterStruct(typeof(PAACallerIdInfo));
			OwaEventRegistry.RegisterStruct(typeof(PAAFindMeInfo));
			OwaEventRegistry.RegisterStruct(typeof(PAATransferToInfo));
			OwaEventRegistry.RegisterHandler(typeof(EditPAAEventHandler));
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x00104C14 File Offset: 0x00102E14
		[OwaEventParameter("CPh", typeof(string), true, true)]
		[OwaEventParameter("Name", typeof(string))]
		[OwaEventParameter("Ext", typeof(string), true, true)]
		[OwaEventParameter("FndMe", typeof(PAAFindMeInfo), true, true)]
		[OwaEventParameter("XfrTo", typeof(PAATransferToInfo), true, true)]
		[OwaEventParameter("Intrpt", typeof(bool), false, true)]
		[OwaEventParameter("OOF", typeof(bool), false, true)]
		[OwaEventParameter("RecVM", typeof(bool), false, true)]
		[OwaEventParameter("CllrId", typeof(PAACallerIdInfo), false, true)]
		[OwaEventParameter("CRcps", typeof(RecipientInfo), true, true)]
		[OwaEvent("Save")]
		[OwaEventParameter("SchS", typeof(int), false, true)]
		[OwaEventParameter("Dur", typeof(PAADurationInfo), false, true)]
		[OwaEventParameter("SvO", typeof(bool), false, true)]
		[OwaEventParameter("Id", typeof(string), false, true)]
		public void Save()
		{
			string text = (string)base.GetParameter("Name");
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
			}
			if (string.IsNullOrEmpty(text))
			{
				base.RenderPartialFailure(-1816947001);
				return;
			}
			if (text.Length > 256)
			{
				text = text.Substring(0, 256);
			}
			string text2 = (string)base.GetParameter("Id");
			using (IPAAStore ipaastore = PAAStore.Create(base.UserContext.ExchangePrincipal))
			{
				IList<PersonalAutoAttendant> list = null;
				ipaastore.TryGetAutoAttendants(PAAValidationMode.None, out list);
				if (list.Count >= 9 && string.IsNullOrEmpty(text2))
				{
					base.RenderPartialFailure(1125061902);
				}
				else
				{
					PersonalAutoAttendant personalAutoAttendant = PersonalAutoAttendant.CreateNew();
					personalAutoAttendant.Name = text;
					string[] array = (string[])base.GetParameter("Ext");
					if (array != null)
					{
						foreach (string item in array)
						{
							personalAutoAttendant.ExtensionList.Add(item);
						}
					}
					if (base.IsParameterSet("SchS"))
					{
						int freeBusy = (int)base.GetParameter("SchS");
						personalAutoAttendant.FreeBusy = (FreeBusyStatusEnum)freeBusy;
					}
					this.AddDuration(personalAutoAttendant);
					this.AddCallerId(personalAutoAttendant);
					if (base.GetParameter("OOF") != null)
					{
						personalAutoAttendant.OutOfOffice = OutOfOfficeStatusEnum.Oof;
					}
					this.AddFindMe(personalAutoAttendant);
					this.AddTransferTo(personalAutoAttendant);
					if (base.GetParameter("RecVM") == null)
					{
						personalAutoAttendant.KeyMappingList.Remove(10);
					}
					personalAutoAttendant.EnableBargeIn = (bool)base.GetParameter("Intrpt");
					personalAutoAttendant.Enabled = true;
					if (string.IsNullOrEmpty(text2))
					{
						list.Add(personalAutoAttendant);
					}
					else
					{
						Guid identity = new Guid(Convert.FromBase64String(text2));
						personalAutoAttendant.Identity = identity;
						PersonalAutoAttendant autoAttendant = ipaastore.GetAutoAttendant(identity, PAAValidationMode.None);
						int index = list.Count;
						if (autoAttendant != null)
						{
							index = list.IndexOf(autoAttendant);
							list.RemoveAt(index);
						}
						list.Insert(index, personalAutoAttendant);
					}
					if (base.IsParameterSet("SvO"))
					{
						this.Writer.Write("<div id=paaId>");
						Utilities.HtmlEncode(Convert.ToBase64String(personalAutoAttendant.Identity.ToByteArray()), this.Writer);
						this.Writer.Write("</div>");
					}
					ipaastore.Save(list);
				}
			}
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x00104E80 File Offset: 0x00103080
		[OwaEvent("VldC")]
		[OwaEventParameter("CllrId", typeof(PAACallerIdInfo), true, false)]
		[OwaEventParameter("CRcps", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("CPh", typeof(string), true, true)]
		public void ValidateCallerId()
		{
			PAACallerIdInfo paacallerIdInfo = (PAACallerIdInfo)base.GetParameter("CllrId");
			string text = null;
			using (IPAAStore ipaastore = PAAStore.Create(base.UserContext.ExchangePrincipal))
			{
				if (paacallerIdInfo.HasPhoneNumbers)
				{
					string[] array = (string[])base.GetParameter("CPh");
					if (array.Length > 50)
					{
						text = Utilities.HtmlEncode(string.Format(CultureInfo.CurrentCulture, LocalizedStrings.GetNonEncoded(1755659442), new object[]
						{
							50
						}));
					}
					if (text == null)
					{
						text = UnifiedMessagingUtilities.ValidatePhoneNumbers(new UnifiedMessagingUtilities.ValidatePhoneNumber(ipaastore.ValidatePhoneNumberCallerId), array);
					}
				}
				IDataValidationResult dataValidationResult = null;
				if (text == null && paacallerIdInfo.HasContacts)
				{
					RecipientInfo[] array2 = (RecipientInfo[])base.GetParameter("CRcps");
					if (array2.Length > 50)
					{
						text = Utilities.HtmlEncode(string.Format(CultureInfo.CurrentCulture, LocalizedStrings.GetNonEncoded(2034140420), new object[]
						{
							50
						}));
					}
					if (text == null)
					{
						foreach (RecipientInfo recipientInfo in array2)
						{
							if (recipientInfo.AddressOrigin == AddressOrigin.Store)
							{
								ipaastore.ValidateContactItemCallerId(recipientInfo.StoreObjectId, out dataValidationResult);
							}
							else if (recipientInfo.AddressOrigin == AddressOrigin.Directory)
							{
								ipaastore.ValidateADContactCallerId(recipientInfo.RoutingAddress, out dataValidationResult);
							}
							if (dataValidationResult.PAAValidationResult != PAAValidationResult.Valid)
							{
								text = UnifiedMessagingUtilities.GetErrorResourceId(dataValidationResult.PAAValidationResult, recipientInfo.DisplayName);
								break;
							}
						}
					}
				}
				if (text == null && paacallerIdInfo.IsInContactFolder)
				{
					ipaastore.ValidateContactFolderCallerId(out dataValidationResult);
					if (dataValidationResult.PAAValidationResult != PAAValidationResult.Valid)
					{
						text = UnifiedMessagingUtilities.GetErrorResourceId(dataValidationResult.PAAValidationResult, null);
					}
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.RenderErrorInfobar(text);
			}
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x0010504C File Offset: 0x0010324C
		[OwaEventParameter("FndMe", typeof(PAAFindMeInfo), false, false)]
		[OwaEvent("VldF")]
		public void ValidateFindMe()
		{
			PAAFindMeInfo paafindMeInfo = (PAAFindMeInfo)base.GetParameter("FndMe");
			string text = null;
			using (IPAAStore ipaastore = PAAStore.Create(base.UserContext.ExchangePrincipal))
			{
				List<string> list = new List<string>();
				if (!string.IsNullOrEmpty(paafindMeInfo.Ph1))
				{
					list.Add(paafindMeInfo.Ph1);
				}
				if (!string.IsNullOrEmpty(paafindMeInfo.Ph2))
				{
					list.Add(paafindMeInfo.Ph2);
				}
				text = UnifiedMessagingUtilities.ValidatePhoneNumbers(new UnifiedMessagingUtilities.ValidatePhoneNumber(ipaastore.ValidatePhoneNumberForOutdialing), list.ToArray());
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.RenderErrorInfobar(text);
			}
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x001050FC File Offset: 0x001032FC
		[OwaEvent("VldT")]
		[OwaEventParameter("XfrTo", typeof(PAATransferToInfo), false, false)]
		public void ValidateTransferTo()
		{
			PAATransferToInfo paatransferToInfo = (PAATransferToInfo)base.GetParameter("XfrTo");
			string text = null;
			using (IPAAStore ipaastore = PAAStore.Create(base.UserContext.ExchangePrincipal))
			{
				if (!string.IsNullOrEmpty(paatransferToInfo.Ph))
				{
					text = UnifiedMessagingUtilities.ValidatePhoneNumbers(new UnifiedMessagingUtilities.ValidatePhoneNumber(ipaastore.ValidatePhoneNumberForOutdialing), new string[]
					{
						paatransferToInfo.Ph
					});
				}
				else
				{
					IDataValidationResult dataValidationResult = null;
					if (paatransferToInfo.VM)
					{
						ipaastore.ValidateADContactForTransferToMailbox(paatransferToInfo.Contact, out dataValidationResult);
					}
					else
					{
						ipaastore.ValidateADContactForOutdialing(paatransferToInfo.Contact, out dataValidationResult);
					}
					if (dataValidationResult.PAAValidationResult != PAAValidationResult.Valid)
					{
						text = UnifiedMessagingUtilities.GetErrorResourceId(dataValidationResult.PAAValidationResult, paatransferToInfo.Contact);
					}
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.RenderErrorInfobar(text);
			}
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x001051D4 File Offset: 0x001033D4
		private void RenderErrorInfobar(string messageHtml)
		{
			this.Writer.Write("<div id=eib>");
			this.Writer.Write(messageHtml);
			this.Writer.Write("</div>");
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x00105204 File Offset: 0x00103404
		private void AddDuration(PersonalAutoAttendant paa)
		{
			PAADurationInfo paadurationInfo = (PAADurationInfo)base.GetParameter("Dur");
			if (paadurationInfo == null)
			{
				return;
			}
			if (paadurationInfo.IsCustomDuration)
			{
				paa.TimeOfDay = TimeOfDayEnum.Custom;
				paa.WorkingPeriod = new WorkingPeriod((DaysOfWeek)paadurationInfo.DaysOfWeek, paadurationInfo.StartTimeMinutes, paadurationInfo.EndTimeMinutes);
				return;
			}
			if (paadurationInfo.IsWorkingHours)
			{
				paa.TimeOfDay = TimeOfDayEnum.WorkingHours;
				return;
			}
			paa.TimeOfDay = TimeOfDayEnum.NonWorkingHours;
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x0010526C File Offset: 0x0010346C
		private void AddCallerId(PersonalAutoAttendant paa)
		{
			PAACallerIdInfo paacallerIdInfo = (PAACallerIdInfo)base.GetParameter("CllrId");
			if (paacallerIdInfo == null)
			{
				return;
			}
			if (paacallerIdInfo.HasPhoneNumbers)
			{
				string[] array = (string[])base.GetParameter("CPh");
				foreach (string phoneNumber in array)
				{
					paa.CallerIdList.Add(CallerIdBase.CreatePhoneNumber(phoneNumber));
				}
			}
			if (paacallerIdInfo.HasContacts)
			{
				RecipientInfo[] array3 = (RecipientInfo[])base.GetParameter("CRcps");
				foreach (RecipientInfo recipientInfo in array3)
				{
					CallerIdBase item = null;
					if (recipientInfo.AddressOrigin == AddressOrigin.Store)
					{
						item = CallerIdBase.CreateContactItem(recipientInfo.StoreObjectId);
					}
					else if (recipientInfo.AddressOrigin == AddressOrigin.Directory)
					{
						item = new ADContactCallerId(recipientInfo.RoutingAddress);
					}
					paa.CallerIdList.Add(item);
				}
			}
			if (paacallerIdInfo.IsInContactFolder)
			{
				paa.CallerIdList.Add(new ContactFolderCallerId());
			}
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x00105364 File Offset: 0x00103564
		private void AddFindMe(PersonalAutoAttendant paa)
		{
			PAAFindMeInfo[] array = (PAAFindMeInfo[])base.GetParameter("FndMe");
			if (array == null)
			{
				return;
			}
			foreach (PAAFindMeInfo paafindMeInfo in array)
			{
				if (!string.IsNullOrEmpty(paafindMeInfo.Ph1))
				{
					paa.KeyMappingList.AddFindMe(paafindMeInfo.Key, paafindMeInfo.Desc, paafindMeInfo.Ph1, paafindMeInfo.Tm1);
				}
				if (!string.IsNullOrEmpty(paafindMeInfo.Ph2))
				{
					paa.KeyMappingList.AddFindMe(paafindMeInfo.Key, paafindMeInfo.Desc, paafindMeInfo.Ph2, paafindMeInfo.Tm2);
				}
			}
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x001053FC File Offset: 0x001035FC
		private void AddTransferTo(PersonalAutoAttendant paa)
		{
			PAATransferToInfo[] array = (PAATransferToInfo[])base.GetParameter("XfrTo");
			if (array == null)
			{
				return;
			}
			foreach (PAATransferToInfo paatransferToInfo in array)
			{
				if (!string.IsNullOrEmpty(paatransferToInfo.Ph))
				{
					paa.KeyMappingList.AddTransferToNumber(paatransferToInfo.Key, paatransferToInfo.Desc, paatransferToInfo.Ph);
				}
				else
				{
					if (string.IsNullOrEmpty(paatransferToInfo.Contact))
					{
						throw new OwaInvalidRequestException("Required field 'Contact' legacy distinguished name is null or empty string");
					}
					if (!Utilities.IsValidLegacyDN(paatransferToInfo.Contact))
					{
						throw new OwaInvalidRequestException(string.Format(CultureInfo.InvariantCulture, "Required field 'Contact' legacy distinguished name is invalid {0}", new object[]
						{
							paatransferToInfo.Contact
						}));
					}
					if (paatransferToInfo.VM)
					{
						paa.KeyMappingList.AddTransferToADContactMailbox(paatransferToInfo.Key, paatransferToInfo.Desc, paatransferToInfo.Contact);
					}
					else
					{
						paa.KeyMappingList.AddTransferToADContactPhone(paatransferToInfo.Key, paatransferToInfo.Desc, paatransferToInfo.Contact);
					}
				}
			}
		}

		// Token: 0x04001F23 RID: 7971
		public const string EventNamespace = "PAA";

		// Token: 0x04001F24 RID: 7972
		public const string MethodSave = "Save";

		// Token: 0x04001F25 RID: 7973
		public const string MethodValidateCallerId = "VldC";

		// Token: 0x04001F26 RID: 7974
		public const string MethodValidateFindMe = "VldF";

		// Token: 0x04001F27 RID: 7975
		public const string MethodValidateTransferTo = "VldT";

		// Token: 0x04001F28 RID: 7976
		public const string Id = "Id";

		// Token: 0x04001F29 RID: 7977
		public const string Name = "Name";

		// Token: 0x04001F2A RID: 7978
		public const string InterruptGreetingParameter = "Intrpt";

		// Token: 0x04001F2B RID: 7979
		public const string OutOfOfficeAssistantParameter = "OOF";

		// Token: 0x04001F2C RID: 7980
		public const string RecordAVoiceMessageParameter = "RecVM";

		// Token: 0x04001F2D RID: 7981
		public const string DialledExtConditionParameter = "Ext";

		// Token: 0x04001F2E RID: 7982
		public const string DurationConditionParameter = "Dur";

		// Token: 0x04001F2F RID: 7983
		public const string CallerIdConditionParameter = "CllrId";

		// Token: 0x04001F30 RID: 7984
		public const string FindMeActionParameter = "FndMe";

		// Token: 0x04001F31 RID: 7985
		public const string TransferToActionParameter = "XfrTo";

		// Token: 0x04001F32 RID: 7986
		public const string CallerIsRecipients = "CRcps";

		// Token: 0x04001F33 RID: 7987
		public const string CallerIsPhoneNumbers = "CPh";

		// Token: 0x04001F34 RID: 7988
		public const string ScheduleStatusConditionParameter = "SchS";

		// Token: 0x04001F35 RID: 7989
		public const string SaveAndStayOpen = "SvO";

		// Token: 0x04001F36 RID: 7990
		private const int MaxNameLength = 256;

		// Token: 0x04001F37 RID: 7991
		private const int MaxCallerIdPhoneNumbers = 50;

		// Token: 0x04001F38 RID: 7992
		private const int MaxCallerIdContacts = 50;

		// Token: 0x04001F39 RID: 7993
		private const int MaxPersonalAutoAttendants = 9;
	}
}
