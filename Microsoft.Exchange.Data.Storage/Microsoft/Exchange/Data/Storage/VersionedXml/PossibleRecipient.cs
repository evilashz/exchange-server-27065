using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000ED3 RID: 3795
	[Serializable]
	public class PossibleRecipient
	{
		// Token: 0x060082E0 RID: 33504 RVA: 0x0023A0BC File Offset: 0x002382BC
		internal static PossibleRecipient GetMathed(IEnumerable<PossibleRecipient> candidates, E164Number number, bool snOnly)
		{
			if (candidates == null)
			{
				throw new ArgumentNullException("candidates");
			}
			if (null == number)
			{
				throw new ArgumentNullException("number");
			}
			foreach (PossibleRecipient possibleRecipient in candidates)
			{
				if (possibleRecipient.Ready && E164Number.Equals(possibleRecipient.PhoneNumber, number, snOnly))
				{
					return possibleRecipient;
				}
			}
			return null;
		}

		// Token: 0x060082E1 RID: 33505 RVA: 0x0023A140 File Offset: 0x00238340
		internal static IList<PossibleRecipient> GetCandidates(IList<PossibleRecipient> recipients, bool effective)
		{
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			List<PossibleRecipient> list = new List<PossibleRecipient>(recipients.Count);
			foreach (PossibleRecipient possibleRecipient in recipients)
			{
				if (possibleRecipient.Ready && effective == possibleRecipient.Effective)
				{
					list.Add(possibleRecipient);
				}
			}
			return new ReadOnlyCollection<PossibleRecipient>(list);
		}

		// Token: 0x060082E2 RID: 33506 RVA: 0x0023A1BC File Offset: 0x002383BC
		internal static void MarkEffective(IEnumerable<PossibleRecipient> recipients, bool effective)
		{
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			foreach (PossibleRecipient possibleRecipient in recipients)
			{
				possibleRecipient.MarkEffective(effective);
			}
		}

		// Token: 0x060082E3 RID: 33507 RVA: 0x0023A214 File Offset: 0x00238414
		internal static int CountTimesSince(List<DateTime> history, DateTime time, bool clearBefore)
		{
			history.Sort();
			int num = history.BinarySearch(time);
			if (0 > num)
			{
				num = -num - 1;
			}
			int result = history.Count - num;
			if (clearBefore)
			{
				history.RemoveRange(0, num);
			}
			return result;
		}

		// Token: 0x060082E4 RID: 33508 RVA: 0x0023A27C File Offset: 0x0023847C
		internal static void PurgeNonEffectiveBefore(List<PossibleRecipient> recipients, DateTime time, int keptAtMost)
		{
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			recipients.Sort(delegate(PossibleRecipient a, PossibleRecipient b)
			{
				if (a.Effective == b.Effective)
				{
					return -DateTime.Compare(a.EffectiveLastModificationTime, b.EffectiveLastModificationTime);
				}
				if (!a.Effective)
				{
					return 1;
				}
				return -1;
			});
			int num = 0;
			int num2 = recipients.Count - 1;
			while (0 <= num2 && !recipients[num2].Effective)
			{
				if (recipients[num2].EffectiveLastModificationTime < time)
				{
					recipients.RemoveAt(num2);
				}
				else
				{
					num++;
				}
				num2--;
			}
			if (num > keptAtMost)
			{
				int num3 = num - keptAtMost;
				recipients.RemoveRange(recipients.Count - 1 - num3, num3);
			}
		}

		// Token: 0x060082E5 RID: 33509 RVA: 0x0023A317 File Offset: 0x00238517
		internal void MarkEffective(bool effective)
		{
			if (this.Effective != effective)
			{
				this.Effective = effective;
				this.EffectiveLastModificationTime = DateTime.UtcNow;
			}
		}

		// Token: 0x060082E6 RID: 33510 RVA: 0x0023A334 File Offset: 0x00238534
		public PossibleRecipient()
		{
		}

		// Token: 0x060082E7 RID: 33511 RVA: 0x0023A33C File Offset: 0x0023853C
		public PossibleRecipient(bool effective, DateTime effectiveLastModificationTime, string region, string carrier, E164Number phonenumber, bool acknowledged, string passcode, List<DateTime> passcodeSentTimeHistory, List<DateTime> passcodeVerificationFailedTimeHistory)
		{
			this.Effective = effective;
			this.EffectiveLastModificationTime = effectiveLastModificationTime;
			this.Region = region;
			this.Carrier = carrier;
			this.PhoneNumber = phonenumber;
			this.Acknowledged = acknowledged;
			this.Passcode = passcode;
			this.PasscodeSentTimeHistory = passcodeSentTimeHistory;
			this.PasscodeVerificationFailedTimeHistory = passcodeVerificationFailedTimeHistory;
		}

		// Token: 0x170022B5 RID: 8885
		// (get) Token: 0x060082E8 RID: 33512 RVA: 0x0023A394 File Offset: 0x00238594
		// (set) Token: 0x060082E9 RID: 33513 RVA: 0x0023A39C File Offset: 0x0023859C
		[XmlElement("Effective")]
		public bool Effective { get; set; }

		// Token: 0x170022B6 RID: 8886
		// (get) Token: 0x060082EA RID: 33514 RVA: 0x0023A3A5 File Offset: 0x002385A5
		// (set) Token: 0x060082EB RID: 33515 RVA: 0x0023A3AD File Offset: 0x002385AD
		[XmlElement("EffectiveLastModificationTime")]
		public DateTime EffectiveLastModificationTime { get; set; }

		// Token: 0x170022B7 RID: 8887
		// (get) Token: 0x060082EC RID: 33516 RVA: 0x0023A3B6 File Offset: 0x002385B6
		// (set) Token: 0x060082ED RID: 33517 RVA: 0x0023A3BE File Offset: 0x002385BE
		[XmlElement("Region")]
		public string Region { get; set; }

		// Token: 0x170022B8 RID: 8888
		// (get) Token: 0x060082EE RID: 33518 RVA: 0x0023A3C7 File Offset: 0x002385C7
		// (set) Token: 0x060082EF RID: 33519 RVA: 0x0023A3CF File Offset: 0x002385CF
		[XmlElement("Carrier")]
		public string Carrier { get; set; }

		// Token: 0x170022B9 RID: 8889
		// (get) Token: 0x060082F0 RID: 33520 RVA: 0x0023A3D8 File Offset: 0x002385D8
		// (set) Token: 0x060082F1 RID: 33521 RVA: 0x0023A3E0 File Offset: 0x002385E0
		[XmlElement("PhoneNumber")]
		public E164Number PhoneNumber { get; set; }

		// Token: 0x170022BA RID: 8890
		// (get) Token: 0x060082F2 RID: 33522 RVA: 0x0023A3E9 File Offset: 0x002385E9
		// (set) Token: 0x060082F3 RID: 33523 RVA: 0x0023A3F1 File Offset: 0x002385F1
		[XmlElement("PhoneNumberSetTime")]
		public DateTime PhoneNumberSetTime { get; set; }

		// Token: 0x170022BB RID: 8891
		// (get) Token: 0x060082F4 RID: 33524 RVA: 0x0023A3FA File Offset: 0x002385FA
		// (set) Token: 0x060082F5 RID: 33525 RVA: 0x0023A402 File Offset: 0x00238602
		[XmlElement("Acknowledged")]
		public bool Acknowledged { get; set; }

		// Token: 0x170022BC RID: 8892
		// (get) Token: 0x060082F6 RID: 33526 RVA: 0x0023A40B File Offset: 0x0023860B
		// (set) Token: 0x060082F7 RID: 33527 RVA: 0x0023A413 File Offset: 0x00238613
		[XmlElement("Passcode")]
		public string Passcode { get; set; }

		// Token: 0x170022BD RID: 8893
		// (get) Token: 0x060082F8 RID: 33528 RVA: 0x0023A41C File Offset: 0x0023861C
		// (set) Token: 0x060082F9 RID: 33529 RVA: 0x0023A429 File Offset: 0x00238629
		[XmlArray("PasscodeSentTimeHistory")]
		[XmlArrayItem("SentTime")]
		public List<DateTime> PasscodeSentTimeHistory
		{
			get
			{
				return AccessorTemplates.ListPropertyGetter<DateTime>(ref this.passcodeSentTimeHistory);
			}
			set
			{
				AccessorTemplates.ListPropertySetter<DateTime>(ref this.passcodeSentTimeHistory, value);
			}
		}

		// Token: 0x170022BE RID: 8894
		// (get) Token: 0x060082FA RID: 33530 RVA: 0x0023A437 File Offset: 0x00238637
		// (set) Token: 0x060082FB RID: 33531 RVA: 0x0023A444 File Offset: 0x00238644
		[XmlArrayItem("FailedTime")]
		[XmlArray("PasscodeVerificationFailedTimeHistory")]
		public List<DateTime> PasscodeVerificationFailedTimeHistory
		{
			get
			{
				return AccessorTemplates.ListPropertyGetter<DateTime>(ref this.passcodeVerificationFailedTimeHistory);
			}
			set
			{
				AccessorTemplates.ListPropertySetter<DateTime>(ref this.passcodeVerificationFailedTimeHistory, value);
			}
		}

		// Token: 0x170022BF RID: 8895
		// (get) Token: 0x060082FC RID: 33532 RVA: 0x0023A452 File Offset: 0x00238652
		[XmlIgnore]
		public bool Ready
		{
			get
			{
				return !string.IsNullOrEmpty(this.Region) && !string.IsNullOrEmpty(this.Carrier) && null != this.PhoneNumber && !string.IsNullOrEmpty(this.PhoneNumber.Number);
			}
		}

		// Token: 0x060082FD RID: 33533 RVA: 0x0023A491 File Offset: 0x00238691
		internal void SetPhonenumber(E164Number number)
		{
			if (this.PhoneNumber != number)
			{
				this.Acknowledged = false;
				this.SetPasscode(null);
			}
			this.PhoneNumber = number;
		}

		// Token: 0x060082FE RID: 33534 RVA: 0x0023A4B6 File Offset: 0x002386B6
		internal void SetPasscode(string passcode)
		{
			this.Passcode = passcode;
			this.PasscodeVerificationFailedTimeHistory.Clear();
		}

		// Token: 0x060082FF RID: 33535 RVA: 0x0023A4CA File Offset: 0x002386CA
		internal void SetAcknowledged(bool acknowledged)
		{
			this.Acknowledged = acknowledged;
			this.PasscodeVerificationFailedTimeHistory.Clear();
		}

		// Token: 0x040057C8 RID: 22472
		private List<DateTime> passcodeSentTimeHistory;

		// Token: 0x040057C9 RID: 22473
		private List<DateTime> passcodeVerificationFailedTimeHistory;
	}
}
