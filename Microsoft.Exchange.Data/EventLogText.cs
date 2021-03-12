using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000FC RID: 252
	[Serializable]
	public struct EventLogText : IComparable, ISerializable
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x0001C600 File Offset: 0x0001A800
		public EventLogText(string input)
		{
			this.value = null;
			if (this.IsValid(input))
			{
				this.value = input;
				return;
			}
			throw new ArgumentOutOfRangeException(DataStrings.EventLogText, DataStrings.InvalidInputErrorMsg);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001C633 File Offset: 0x0001A833
		public static EventLogText Parse(string s)
		{
			return new EventLogText(s);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001C63C File Offset: 0x0001A83C
		private EventLogText(SerializationInfo info, StreamingContext context)
		{
			this.value = (string)info.GetValue("value", typeof(string));
			if (!this.IsValid(this.value))
			{
				throw new ArgumentOutOfRangeException(DataStrings.EventLogText, this.value.ToString());
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001C692 File Offset: 0x0001A892
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("value", this.value);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001C6A5 File Offset: 0x0001A8A5
		private bool IsValid(string input)
		{
			return input == null || (input.Length <= 128 && EventLogText.ValidatingExpression.IsMatch(input));
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0001C6C8 File Offset: 0x0001A8C8
		public static EventLogText Empty
		{
			get
			{
				return default(EventLogText);
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0001C6DE File Offset: 0x0001A8DE
		public string Value
		{
			get
			{
				if (this.IsValid(this.value))
				{
					return this.value;
				}
				throw new ArgumentOutOfRangeException("Value", this.value.ToString());
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001C70A File Offset: 0x0001A90A
		public override string ToString()
		{
			if (this.value == null)
			{
				return string.Empty;
			}
			return this.value.ToString();
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001C725 File Offset: 0x0001A925
		public override int GetHashCode()
		{
			if (this.value == null)
			{
				return string.Empty.GetHashCode();
			}
			return this.value.GetHashCode();
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001C745 File Offset: 0x0001A945
		public override bool Equals(object obj)
		{
			return obj is EventLogText && this.Equals((EventLogText)obj);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001C75D File Offset: 0x0001A95D
		public bool Equals(EventLogText obj)
		{
			return this.value == obj.Value;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001C771 File Offset: 0x0001A971
		public static bool operator ==(EventLogText a, EventLogText b)
		{
			return a.Value == b.Value;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001C786 File Offset: 0x0001A986
		public static bool operator !=(EventLogText a, EventLogText b)
		{
			return a.Value != b.Value;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001C79C File Offset: 0x0001A99C
		public int CompareTo(object obj)
		{
			if (!(obj is EventLogText))
			{
				throw new ArgumentException("Parameter is not of type EventLogText.");
			}
			return string.Compare(this.value, ((EventLogText)obj).Value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040005BC RID: 1468
		public const int MaxLength = 128;

		// Token: 0x040005BD RID: 1469
		public const string AllowedCharacters = "(.|[^.])";

		// Token: 0x040005BE RID: 1470
		public static readonly Regex ValidatingExpression = new Regex("^(.|[^.])+$", RegexOptions.Compiled);

		// Token: 0x040005BF RID: 1471
		private string value;
	}
}
