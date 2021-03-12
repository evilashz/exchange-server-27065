using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009E6 RID: 2534
	[Serializable]
	public class ExTimeZoneValue : IEquatable<ExTimeZoneValue>, ISerializable
	{
		// Token: 0x17001976 RID: 6518
		// (get) Token: 0x06005CE2 RID: 23778 RVA: 0x00184554 File Offset: 0x00182754
		// (set) Token: 0x06005CE3 RID: 23779 RVA: 0x0018455C File Offset: 0x0018275C
		public ExTimeZone ExTimeZone
		{
			get
			{
				return this.exTimeZone;
			}
			private set
			{
				this.exTimeZone = value;
			}
		}

		// Token: 0x06005CE4 RID: 23780 RVA: 0x00184565 File Offset: 0x00182765
		public ExTimeZoneValue(ExTimeZone timeZone)
		{
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}
			this.ExTimeZone = timeZone;
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x00184584 File Offset: 0x00182784
		public ExTimeZoneValue(TimeZoneInfo timeZoneInfo)
		{
			foreach (ExTimeZone exTimeZone in ExTimeZoneEnumerator.Instance)
			{
				if (string.Equals(exTimeZone.DisplayName, timeZoneInfo.DisplayName, StringComparison.OrdinalIgnoreCase))
				{
					this.ExTimeZone = exTimeZone;
					break;
				}
			}
			if (this.ExTimeZone == null)
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06005CE6 RID: 23782 RVA: 0x001845FC File Offset: 0x001827FC
		protected ExTimeZoneValue(SerializationInfo information, StreamingContext context)
		{
			string @string = information.GetString("TimeZoneId");
			this.ExTimeZone = default(ExTimeZoneValue.StringParser).Parse(@string);
		}

		// Token: 0x06005CE7 RID: 23783 RVA: 0x00184634 File Offset: 0x00182834
		public static ExTimeZoneValue Parse(string timeZoneString)
		{
			return new ExTimeZoneValue(default(ExTimeZoneValue.StringParser).Parse(timeZoneString));
		}

		// Token: 0x06005CE8 RID: 23784 RVA: 0x00184656 File Offset: 0x00182856
		public static bool operator ==(ExTimeZoneValue left, ExTimeZoneValue right)
		{
			if (left != null)
			{
				return left.Equals(right);
			}
			return right == null;
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x00184667 File Offset: 0x00182867
		public static bool operator !=(ExTimeZoneValue left, ExTimeZoneValue right)
		{
			return !(left == right);
		}

		// Token: 0x06005CEA RID: 23786 RVA: 0x00184674 File Offset: 0x00182874
		public static bool TryParse(string timeZoneString, out ExTimeZoneValue instance)
		{
			ExTimeZone timeZone;
			bool flag = default(ExTimeZoneValue.StringParser).TryParse(timeZoneString, out timeZone);
			instance = (flag ? new ExTimeZoneValue(timeZone) : null);
			return flag;
		}

		// Token: 0x06005CEB RID: 23787 RVA: 0x001846A3 File Offset: 0x001828A3
		public override string ToString()
		{
			return this.ExTimeZone.Id;
		}

		// Token: 0x06005CEC RID: 23788 RVA: 0x001846B0 File Offset: 0x001828B0
		public bool Equals(ExTimeZoneValue other)
		{
			return other != null && this.ExTimeZone == other.ExTimeZone;
		}

		// Token: 0x06005CED RID: 23789 RVA: 0x001846C5 File Offset: 0x001828C5
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ExTimeZoneValue);
		}

		// Token: 0x06005CEE RID: 23790 RVA: 0x001846D3 File Offset: 0x001828D3
		public override int GetHashCode()
		{
			return this.ExTimeZone.GetHashCode();
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x001846E0 File Offset: 0x001828E0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("TimeZoneId", this.ExTimeZone.Id, typeof(string));
		}

		// Token: 0x040033F9 RID: 13305
		public const string GMTFormatPrefix = "GMT";

		// Token: 0x040033FA RID: 13306
		private const string TimeZoneId = "TimeZoneId";

		// Token: 0x040033FB RID: 13307
		[NonSerialized]
		private ExTimeZone exTimeZone;

		// Token: 0x020009E7 RID: 2535
		private struct StringParser
		{
			// Token: 0x06005CF0 RID: 23792 RVA: 0x00184704 File Offset: 0x00182904
			internal bool TryParse(string s, out ExTimeZone timeZone)
			{
				this.error = ExTimeZoneValue.StringParser.ParseError.NoError;
				this.multipleMatches = string.Empty;
				if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(s, out timeZone))
				{
					Match match = Regex.Match(s, "^GMT([+|-]?)([^+-]+)");
					if (match.Success)
					{
						string s2 = (match.Groups[1].Value == "-") ? ("-" + match.Groups[2].Value) : match.Groups[2].Value;
						TimeSpan t;
						if (TimeSpan.TryParse(s2, out t))
						{
							List<ExTimeZone> list = new List<ExTimeZone>();
							foreach (ExTimeZone exTimeZone in ExTimeZoneEnumerator.Instance)
							{
								if (exTimeZone.TimeZoneInformation.StandardBias == t)
								{
									list.Add(exTimeZone);
								}
							}
							if (list.Count == 1)
							{
								timeZone = list[0];
								goto IL_168;
							}
							if (list.Count == 0)
							{
								this.error = ExTimeZoneValue.StringParser.ParseError.NoGmtMatch;
								goto IL_168;
							}
							this.error = ExTimeZoneValue.StringParser.ParseError.MultipleGmtMatches;
							using (List<ExTimeZone>.Enumerator enumerator2 = list.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									ExTimeZone exTimeZone2 = enumerator2.Current;
									this.multipleMatches = this.multipleMatches + "\n\t" + exTimeZone2.Id;
								}
								goto IL_168;
							}
						}
						this.error = ExTimeZoneValue.StringParser.ParseError.WrongGmtFormat;
					}
					else
					{
						this.error = ExTimeZoneValue.StringParser.ParseError.TimeZoneNotFound;
					}
				}
				IL_168:
				return this.error == ExTimeZoneValue.StringParser.ParseError.NoError;
			}

			// Token: 0x06005CF1 RID: 23793 RVA: 0x001848A0 File Offset: 0x00182AA0
			internal ExTimeZone Parse(string s)
			{
				ExTimeZone result = null;
				if (!this.TryParse(s, out result))
				{
					switch (this.error)
					{
					case ExTimeZoneValue.StringParser.ParseError.TimeZoneNotFound:
						throw new FormatException(ServerStrings.ErrorExTimeZoneValueTimeZoneNotFound);
					case ExTimeZoneValue.StringParser.ParseError.WrongGmtFormat:
						throw new FormatException(ServerStrings.ErrorExTimeZoneValueWrongGmtFormat);
					case ExTimeZoneValue.StringParser.ParseError.MultipleGmtMatches:
						throw new FormatException(ServerStrings.ErrorExTimeZoneValueMultipleGmtMatches(this.multipleMatches));
					case ExTimeZoneValue.StringParser.ParseError.NoGmtMatch:
						throw new FormatException(ServerStrings.ErrorExTimeZoneValueNoGmtMatch);
					}
				}
				return result;
			}

			// Token: 0x040033FC RID: 13308
			private ExTimeZoneValue.StringParser.ParseError error;

			// Token: 0x040033FD RID: 13309
			private string multipleMatches;

			// Token: 0x020009E8 RID: 2536
			private enum ParseError
			{
				// Token: 0x040033FF RID: 13311
				NoError,
				// Token: 0x04003400 RID: 13312
				TimeZoneNotFound,
				// Token: 0x04003401 RID: 13313
				WrongGmtFormat,
				// Token: 0x04003402 RID: 13314
				MultipleGmtMatches,
				// Token: 0x04003403 RID: 13315
				NoGmtMatch
			}
		}
	}
}
