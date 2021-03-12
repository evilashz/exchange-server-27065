using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Internal;

namespace Microsoft.Exchange.Data.ContentTypes.iCalendar
{
	// Token: 0x020000DB RID: 219
	public class Recurrence
	{
		// Token: 0x0600087E RID: 2174 RVA: 0x0002EB60 File Offset: 0x0002CD60
		static Recurrence()
		{
			Recurrence.frequencyToEnumTable.Add("SECONDLY", Frequency.Secondly);
			Recurrence.frequencyToEnumTable.Add("MINUTELY", Frequency.Minutely);
			Recurrence.frequencyToEnumTable.Add("HOURLY", Frequency.Hourly);
			Recurrence.frequencyToEnumTable.Add("DAILY", Frequency.Daily);
			Recurrence.frequencyToEnumTable.Add("WEEKLY", Frequency.Weekly);
			Recurrence.frequencyToEnumTable.Add("MONTHLY", Frequency.Monthly);
			Recurrence.frequencyToEnumTable.Add("YEARLY", Frequency.Yearly);
			Recurrence.recurPropToEnumTable.Add("FREQ", RecurrenceProperties.Frequency);
			Recurrence.recurPropToEnumTable.Add("UNTIL", RecurrenceProperties.UntilDate);
			Recurrence.recurPropToEnumTable.Add("COUNT", RecurrenceProperties.Count);
			Recurrence.recurPropToEnumTable.Add("INTERVAL", RecurrenceProperties.Interval);
			Recurrence.recurPropToEnumTable.Add("BYSECOND", RecurrenceProperties.BySecond);
			Recurrence.recurPropToEnumTable.Add("BYMINUTE", RecurrenceProperties.ByMinute);
			Recurrence.recurPropToEnumTable.Add("BYHOUR", RecurrenceProperties.ByHour);
			Recurrence.recurPropToEnumTable.Add("BYDAY", RecurrenceProperties.ByDay);
			Recurrence.recurPropToEnumTable.Add("BYMONTHDAY", RecurrenceProperties.ByMonthDay);
			Recurrence.recurPropToEnumTable.Add("BYYEARDAY", RecurrenceProperties.ByYearDay);
			Recurrence.recurPropToEnumTable.Add("BYWEEKNO", RecurrenceProperties.ByWeek);
			Recurrence.recurPropToEnumTable.Add("BYMONTH", RecurrenceProperties.ByMonth);
			Recurrence.recurPropToEnumTable.Add("BYSETPOS", RecurrenceProperties.BySetPosition);
			Recurrence.recurPropToEnumTable.Add("WKST", RecurrenceProperties.WeekStart);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0002ECF0 File Offset: 0x0002CEF0
		public Recurrence()
		{
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0002ED06 File Offset: 0x0002CF06
		public Recurrence(string value) : this(value, null)
		{
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0002ED10 File Offset: 0x0002CF10
		internal Recurrence(string s, ComplianceTracker tracker)
		{
			this.tracker = tracker;
			Recurrence.ParserStates parserStates = Recurrence.ParserStates.Name;
			int length = s.Length;
			string s2 = string.Empty;
			List<string> list = new List<string>();
			int i = 0;
			while (i < length)
			{
				switch (parserStates)
				{
				case Recurrence.ParserStates.Name:
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (i < length)
					{
						char c = s[i++];
						if ((int)c >= ContentLineParser.Dictionary.Length || (byte)(ContentLineParser.Dictionary[(int)c] & ContentLineParser.Tokens.ValueChar) == 0)
						{
							this.SetComplianceStatus(CalendarStrings.InvalidCharacterInRecurrence);
							return;
						}
						if (c == '=')
						{
							break;
						}
						stringBuilder.Append(c);
					}
					s2 = stringBuilder.ToString();
					parserStates = Recurrence.ParserStates.Value;
					break;
				}
				case Recurrence.ParserStates.Value:
				{
					bool flag = false;
					StringBuilder stringBuilder = new StringBuilder();
					while (i < length)
					{
						char c = s[i++];
						if ((int)c >= ContentLineParser.Dictionary.Length || (byte)(ContentLineParser.Dictionary[(int)c] & ContentLineParser.Tokens.ValueChar) == 0)
						{
							this.SetComplianceStatus(CalendarStrings.InvalidCharacterInRecurrence);
							return;
						}
						if (c == ';')
						{
							flag = true;
							parserStates = Recurrence.ParserStates.Name;
							break;
						}
						if (c == ',')
						{
							flag = false;
							break;
						}
						stringBuilder.Append(c);
					}
					list.Add(stringBuilder.ToString());
					if (flag || i == length)
					{
						int num = list.Count;
						RecurrenceProperties recurProp = Recurrence.GetRecurProp(s2);
						if (recurProp <= RecurrenceProperties.ByDay)
						{
							if (recurProp <= RecurrenceProperties.BySecond)
							{
								switch (recurProp)
								{
								case RecurrenceProperties.Frequency:
									if (num > 1)
									{
										this.SetComplianceStatus(CalendarStrings.MultivalueNotPermittedOnFreq);
										return;
									}
									this.freq = Recurrence.GetFrequency(list[0]);
									if (this.freq == Frequency.Unknown)
									{
										this.SetComplianceStatus(CalendarStrings.UnknownFrequencyValue);
										return;
									}
									this.props |= RecurrenceProperties.Frequency;
									break;
								case RecurrenceProperties.UntilDate:
									if (num > 1)
									{
										this.SetComplianceStatus(CalendarStrings.MultivalueNotPermittedOnUntil);
										return;
									}
									if ((this.props & RecurrenceProperties.UntilDate) != RecurrenceProperties.None || (this.props & RecurrenceProperties.UntilDateTime) != RecurrenceProperties.None)
									{
										this.SetComplianceStatus(CalendarStrings.UntilOnlyPermittedOnce);
										return;
									}
									if ((this.props & RecurrenceProperties.Count) != RecurrenceProperties.None)
									{
										this.SetComplianceStatus(CalendarStrings.UntilNotPermittedWithCount);
										return;
									}
									if (list[0].Length > 8)
									{
										this.untilDateTime = CalendarCommon.ParseDateTime(list[0], tracker);
										this.props |= RecurrenceProperties.UntilDateTime;
									}
									else
									{
										this.untilDate = CalendarCommon.ParseDate(list[0], tracker);
										this.props |= RecurrenceProperties.UntilDate;
									}
									break;
								case RecurrenceProperties.Frequency | RecurrenceProperties.UntilDate:
									goto IL_A5C;
								case RecurrenceProperties.Count:
									if ((this.props & RecurrenceProperties.Count) != RecurrenceProperties.None)
									{
										this.SetComplianceStatus(CalendarStrings.CountOnlyPermittedOnce);
										return;
									}
									if ((this.props & RecurrenceProperties.UntilDate) != RecurrenceProperties.None || (this.props & RecurrenceProperties.UntilDateTime) != RecurrenceProperties.None)
									{
										this.SetComplianceStatus(CalendarStrings.CountNotPermittedWithUntil);
										return;
									}
									if (num > 1)
									{
										this.SetComplianceStatus(CalendarStrings.MultivalueNotPermittedOnCount);
										return;
									}
									if (!int.TryParse(list[0], out this.count))
									{
										this.SetComplianceStatus(CalendarStrings.InvalidValueFormat);
									}
									this.props |= RecurrenceProperties.Count;
									break;
								default:
									if (recurProp != RecurrenceProperties.Interval)
									{
										if (recurProp != RecurrenceProperties.BySecond)
										{
											goto IL_A5C;
										}
										if ((this.props & RecurrenceProperties.BySecond) != RecurrenceProperties.None)
										{
											this.SetComplianceStatus(CalendarStrings.BySecondOnlyPermittedOnce);
											return;
										}
										this.bySecond = new int[num];
										for (int j = 0; j < num; j++)
										{
											if (!int.TryParse(list[j], out this.bySecond[j]))
											{
												this.SetComplianceStatus(CalendarStrings.InvalidValueFormat);
											}
											if (this.bySecond[j] < 0 || this.bySecond[j] > 59)
											{
												this.SetComplianceStatus(CalendarStrings.BySecondOutOfRange);
												return;
											}
										}
										this.props |= RecurrenceProperties.BySecond;
									}
									else
									{
										if ((this.props & RecurrenceProperties.Interval) != RecurrenceProperties.None)
										{
											this.SetComplianceStatus(CalendarStrings.IntervalOnlyPermittedOnce);
											return;
										}
										if (num > 1)
										{
											this.SetComplianceStatus(CalendarStrings.MultivalueNotPermittedOnInterval);
											return;
										}
										if (!int.TryParse(list[0], out this.interval))
										{
											this.SetComplianceStatus(CalendarStrings.InvalidValueFormat);
										}
										if (this.interval < 1)
										{
											this.SetComplianceStatus(CalendarStrings.IntervalMustBePositive);
											return;
										}
										this.props |= RecurrenceProperties.Interval;
									}
									break;
								}
							}
							else if (recurProp != RecurrenceProperties.ByMinute)
							{
								if (recurProp != RecurrenceProperties.ByHour)
								{
									if (recurProp != RecurrenceProperties.ByDay)
									{
										goto IL_A5C;
									}
									if ((this.props & RecurrenceProperties.ByDay) != RecurrenceProperties.None)
									{
										this.SetComplianceStatus(CalendarStrings.ByDayOnlyPermittedOnce);
										return;
									}
									this.byDay = new Recurrence.ByDay[num];
									for (int k = 0; k < num; k++)
									{
										string text = list[k];
										if (text.Length != 0)
										{
											int num2 = 0;
											while (num2 < text.Length && text[num2] == ' ')
											{
												num2++;
											}
											if (num2 != text.Length)
											{
												int num3 = num2 - 1;
												char c2;
												do
												{
													c2 = text[++num3];
													if ((int)c2 >= ContentLineParser.Dictionary.Length)
													{
														goto Block_53;
													}
												}
												while (((byte)(ContentLineParser.Dictionary[(int)c2] & ContentLineParser.Tokens.Digit) > 0 || c2 == '+' || c2 == '-') && num3 + 1 < text.Length);
												IL_66C:
												if (num3 != num2)
												{
													int num4 = 0;
													string s3 = text.Substring(num2, num3 - num2);
													if (!int.TryParse(s3, out num4) || num4 == 0)
													{
														this.SetComplianceStatus(CalendarStrings.InvalidValueFormat);
													}
													this.byDay[k].OccurrenceNumber = num4;
												}
												while (text[num3] == ' ' && num3 + 1 < text.Length)
												{
													num3++;
												}
												this.byDay[k].Day = this.GetDayOfWeek(text.Substring(num3, text.Length - num3));
												goto IL_700;
												Block_53:
												this.SetComplianceStatus(CalendarStrings.InvalidValueFormat);
												goto IL_66C;
											}
										}
										IL_700:;
									}
									this.props |= RecurrenceProperties.ByDay;
								}
								else
								{
									if ((this.props & RecurrenceProperties.ByHour) != RecurrenceProperties.None)
									{
										this.SetComplianceStatus(CalendarStrings.ByHourOnlyPermittedOnce);
										return;
									}
									this.byHour = new int[num];
									for (int l = 0; l < num; l++)
									{
										if (!int.TryParse(list[l], out this.byHour[l]))
										{
											this.SetComplianceStatus(CalendarStrings.InvalidValueFormat);
										}
										if (this.byHour[l] < 0 || this.byHour[l] > 23)
										{
											this.SetComplianceStatus(CalendarStrings.ByHourOutOfRange);
											return;
										}
									}
									this.props |= RecurrenceProperties.ByHour;
								}
							}
							else
							{
								if ((this.props & RecurrenceProperties.ByMinute) != RecurrenceProperties.None)
								{
									this.SetComplianceStatus(CalendarStrings.ByMinuteOnlyPermittedOnce);
									return;
								}
								this.byMinute = new int[num];
								for (int m = 0; m < num; m++)
								{
									if (!int.TryParse(list[m], out this.byMinute[m]))
									{
										this.SetComplianceStatus(CalendarStrings.InvalidValueFormat);
									}
									if (this.byMinute[m] < 0 || this.byMinute[m] > 59)
									{
										this.SetComplianceStatus(CalendarStrings.ByMinuteOutOfRange);
										return;
									}
								}
								this.props |= RecurrenceProperties.ByMinute;
							}
						}
						else if (recurProp <= RecurrenceProperties.ByWeek)
						{
							if (recurProp != RecurrenceProperties.ByMonthDay)
							{
								if (recurProp != RecurrenceProperties.ByYearDay)
								{
									if (recurProp != RecurrenceProperties.ByWeek)
									{
										goto IL_A5C;
									}
									if ((this.props & RecurrenceProperties.ByWeek) != RecurrenceProperties.None)
									{
										this.SetComplianceStatus(CalendarStrings.ByWeekNoOnlyPermittedOnce);
										return;
									}
									this.byWeekNumber = new int[num];
									for (int n = 0; n < num; n++)
									{
										int num5;
										if (!int.TryParse(list[n], out num5))
										{
											this.SetComplianceStatus(CalendarStrings.InvalidValueFormat);
										}
										this.byWeekNumber[n] = num5;
										if (num5 == 0 || num5 > 53 || num5 < -53)
										{
											this.SetComplianceStatus(CalendarStrings.ByWeekNoOutOfRange);
											return;
										}
									}
									this.props |= RecurrenceProperties.ByWeek;
								}
								else
								{
									if ((this.props & RecurrenceProperties.ByYearDay) != RecurrenceProperties.None)
									{
										this.SetComplianceStatus(CalendarStrings.ByYearDayOnlyPermittedOnce);
										return;
									}
									this.byYearDay = new int[num];
									for (int num6 = 0; num6 < num; num6++)
									{
										int num7;
										if (!int.TryParse(list[num6], out num7))
										{
											this.SetComplianceStatus(CalendarStrings.InvalidValueFormat);
										}
										this.byYearDay[num6] = num7;
										if (num7 == 0 || num7 > 366 || num7 < -366)
										{
											this.SetComplianceStatus(CalendarStrings.ByYearDayOutOfRange);
											return;
										}
									}
									this.props |= RecurrenceProperties.ByYearDay;
								}
							}
							else
							{
								if ((this.props & RecurrenceProperties.ByMonthDay) != RecurrenceProperties.None)
								{
									this.SetComplianceStatus(CalendarStrings.ByMonthDayOnlyPermittedOnce);
									return;
								}
								this.byMonthDay = new int[num];
								for (int num8 = 0; num8 < num; num8++)
								{
									int num9;
									if (!int.TryParse(list[num8], out num9))
									{
										this.SetComplianceStatus(CalendarStrings.InvalidValueFormat);
									}
									this.byMonthDay[num8] = num9;
									if (num9 == 0 || num9 > 31 || num9 < -31)
									{
										this.SetComplianceStatus(CalendarStrings.ByMonthDayOutOfRange);
										return;
									}
								}
								this.props |= RecurrenceProperties.ByMonthDay;
							}
						}
						else if (recurProp != RecurrenceProperties.ByMonth)
						{
							if (recurProp != RecurrenceProperties.BySetPosition)
							{
								if (recurProp != RecurrenceProperties.WeekStart)
								{
									goto IL_A5C;
								}
								if ((this.props & RecurrenceProperties.WeekStart) != RecurrenceProperties.None)
								{
									this.SetComplianceStatus(CalendarStrings.WkStOnlyPermittedOnce);
									return;
								}
								if (num > 1)
								{
									this.SetComplianceStatus(CalendarStrings.MultivalueNotPermittedOnWkSt);
									return;
								}
								this.workWeekStart = this.GetDayOfWeek(list[0]);
								this.props |= RecurrenceProperties.WeekStart;
							}
							else
							{
								if ((this.props & RecurrenceProperties.BySetPosition) != RecurrenceProperties.None)
								{
									this.SetComplianceStatus(CalendarStrings.BySetPosOnlyPermittedOnce);
									return;
								}
								this.bySetPos = new int[num];
								for (int num10 = 0; num10 < num; num10++)
								{
									int num11;
									if (!int.TryParse(list[num10], out num11))
									{
										this.SetComplianceStatus(CalendarStrings.InvalidValueFormat);
									}
									this.bySetPos[num10] = num11;
									if (num11 == 0 || num11 > 366 || num11 < -366)
									{
										this.SetComplianceStatus(CalendarStrings.BySetPosOutOfRange);
										return;
									}
								}
								this.props |= RecurrenceProperties.BySetPosition;
							}
						}
						else
						{
							if ((this.props & RecurrenceProperties.ByMonth) != RecurrenceProperties.None)
							{
								this.SetComplianceStatus(CalendarStrings.ByMonthOnlyPermittedOnce);
								return;
							}
							this.byMonth = new int[num];
							for (int num12 = 0; num12 < num; num12++)
							{
								int num13;
								if (!int.TryParse(list[num12], out num13))
								{
									this.SetComplianceStatus(CalendarStrings.InvalidValueFormat);
								}
								this.byMonth[num12] = num13;
								if (num13 < 0 || num13 > 12)
								{
									this.SetComplianceStatus(CalendarStrings.ByMonthOutOfRange);
									return;
								}
							}
							this.props |= RecurrenceProperties.ByMonth;
						}
						IL_A67:
						list = new List<string>();
						break;
						IL_A5C:
						this.SetComplianceStatus(CalendarStrings.UnknownRecurrenceProperty);
						goto IL_A67;
					}
					break;
				}
				}
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0002F793 File Offset: 0x0002D993
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x0002F79B File Offset: 0x0002D99B
		public Frequency Frequency
		{
			get
			{
				return this.freq;
			}
			set
			{
				this.props |= RecurrenceProperties.Frequency;
				this.freq = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0002F7B2 File Offset: 0x0002D9B2
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x0002F7BA File Offset: 0x0002D9BA
		public DateTime UntilDate
		{
			get
			{
				return this.untilDate;
			}
			set
			{
				this.props &= ~RecurrenceProperties.UntilDateTime;
				this.props |= RecurrenceProperties.UntilDate;
				this.untilDate = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0002F7E3 File Offset: 0x0002D9E3
		// (set) Token: 0x06000887 RID: 2183 RVA: 0x0002F7EB File Offset: 0x0002D9EB
		public DateTime UntilDateTime
		{
			get
			{
				return this.untilDateTime;
			}
			set
			{
				this.props &= ~RecurrenceProperties.UntilDate;
				this.props |= RecurrenceProperties.UntilDateTime;
				this.untilDateTime = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0002F815 File Offset: 0x0002DA15
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x0002F81D File Offset: 0x0002DA1D
		public int Count
		{
			get
			{
				return this.count;
			}
			set
			{
				this.props |= RecurrenceProperties.Count;
				this.count = value;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0002F834 File Offset: 0x0002DA34
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x0002F83C File Offset: 0x0002DA3C
		public int Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				this.props |= RecurrenceProperties.Interval;
				this.interval = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0002F853 File Offset: 0x0002DA53
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x0002F85B File Offset: 0x0002DA5B
		public int[] BySecond
		{
			get
			{
				return this.bySecond;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.props |= RecurrenceProperties.BySecond;
				this.bySecond = value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0002F87C File Offset: 0x0002DA7C
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x0002F884 File Offset: 0x0002DA84
		public int[] ByMinute
		{
			get
			{
				return this.byMinute;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.props |= RecurrenceProperties.ByMinute;
				this.byMinute = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0002F8A5 File Offset: 0x0002DAA5
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x0002F8AD File Offset: 0x0002DAAD
		public int[] ByHour
		{
			get
			{
				return this.byHour;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.props |= RecurrenceProperties.ByHour;
				this.byHour = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0002F8CE File Offset: 0x0002DACE
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x0002F8D6 File Offset: 0x0002DAD6
		public Recurrence.ByDay[] ByDayList
		{
			get
			{
				return this.byDay;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.props |= RecurrenceProperties.ByDay;
				this.byDay = value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0002F8FA File Offset: 0x0002DAFA
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x0002F902 File Offset: 0x0002DB02
		public int[] ByMonthDay
		{
			get
			{
				return this.byMonthDay;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.props |= RecurrenceProperties.ByMonthDay;
				this.byMonthDay = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x0002F926 File Offset: 0x0002DB26
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x0002F92E File Offset: 0x0002DB2E
		public int[] ByYearDay
		{
			get
			{
				return this.byYearDay;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.props |= RecurrenceProperties.ByYearDay;
				this.byYearDay = value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0002F952 File Offset: 0x0002DB52
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x0002F95A File Offset: 0x0002DB5A
		public int[] ByWeek
		{
			get
			{
				return this.byWeekNumber;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.props |= RecurrenceProperties.ByWeek;
				this.byWeekNumber = value;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0002F97E File Offset: 0x0002DB7E
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x0002F986 File Offset: 0x0002DB86
		public int[] ByMonth
		{
			get
			{
				return this.byMonth;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.props |= RecurrenceProperties.ByMonth;
				this.byMonth = value;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0002F9AA File Offset: 0x0002DBAA
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x0002F9B2 File Offset: 0x0002DBB2
		public int[] BySetPosition
		{
			get
			{
				return this.bySetPos;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.props |= RecurrenceProperties.BySetPosition;
				this.bySetPos = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0002F9D6 File Offset: 0x0002DBD6
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x0002F9DE File Offset: 0x0002DBDE
		public DayOfWeek WorkWeekStart
		{
			get
			{
				return this.workWeekStart;
			}
			set
			{
				this.props |= RecurrenceProperties.WeekStart;
				this.workWeekStart = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0002F9F9 File Offset: 0x0002DBF9
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x0002FA01 File Offset: 0x0002DC01
		public RecurrenceProperties AvailableProperties
		{
			get
			{
				return this.props;
			}
			set
			{
				this.props = value;
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002FA0C File Offset: 0x0002DC0C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if ((this.props & RecurrenceProperties.Frequency) != RecurrenceProperties.None)
			{
				stringBuilder.Append("FREQ");
				stringBuilder.Append('=');
				stringBuilder.Append(Recurrence.GetFrequencyString(this.freq));
			}
			if ((this.props & RecurrenceProperties.UntilDate) != RecurrenceProperties.None)
			{
				stringBuilder.Append(";UNTIL");
				stringBuilder.Append('=');
				stringBuilder.Append(CalendarCommon.FormatDate(this.untilDate));
			}
			if ((this.props & RecurrenceProperties.UntilDateTime) != RecurrenceProperties.None)
			{
				stringBuilder.Append(";UNTIL");
				stringBuilder.Append('=');
				stringBuilder.Append(CalendarCommon.FormatDateTime(this.untilDateTime));
			}
			if ((this.props & RecurrenceProperties.Count) != RecurrenceProperties.None)
			{
				stringBuilder.Append(";COUNT");
				stringBuilder.Append('=');
				stringBuilder.Append(this.count);
			}
			if ((this.props & RecurrenceProperties.Interval) != RecurrenceProperties.None)
			{
				stringBuilder.Append(";INTERVAL");
				stringBuilder.Append('=');
				stringBuilder.Append(this.interval);
			}
			if ((this.props & RecurrenceProperties.BySecond) != RecurrenceProperties.None)
			{
				stringBuilder.Append(";BYSECOND");
				stringBuilder.Append('=');
				this.OutputList(this.bySecond, stringBuilder);
			}
			if ((this.props & RecurrenceProperties.ByMinute) != RecurrenceProperties.None)
			{
				stringBuilder.Append(";BYMINUTE");
				stringBuilder.Append('=');
				this.OutputList(this.byMinute, stringBuilder);
			}
			if ((this.props & RecurrenceProperties.ByHour) != RecurrenceProperties.None)
			{
				stringBuilder.Append(";BYHOUR");
				stringBuilder.Append('=');
				this.OutputList(this.byHour, stringBuilder);
			}
			if ((this.props & RecurrenceProperties.ByDay) != RecurrenceProperties.None)
			{
				stringBuilder.Append(";BYDAY");
				stringBuilder.Append('=');
				int num = this.byDay.Length;
				if (num > 0)
				{
					stringBuilder.Append(this.byDay[0]);
				}
				for (int i = 1; i < num; i++)
				{
					stringBuilder.Append(',');
					stringBuilder.Append(this.byDay[i].ToString());
				}
			}
			if ((this.props & RecurrenceProperties.ByMonthDay) != RecurrenceProperties.None)
			{
				stringBuilder.Append(";BYMONTHDAY");
				stringBuilder.Append('=');
				this.OutputList(this.byMonthDay, stringBuilder);
			}
			if ((this.props & RecurrenceProperties.ByYearDay) != RecurrenceProperties.None)
			{
				stringBuilder.Append(";BYYEARDAY");
				stringBuilder.Append('=');
				this.OutputList(this.byYearDay, stringBuilder);
			}
			if ((this.props & RecurrenceProperties.ByWeek) != RecurrenceProperties.None)
			{
				stringBuilder.Append(";BYWEEKNO");
				stringBuilder.Append('=');
				this.OutputList(this.byWeekNumber, stringBuilder);
			}
			if ((this.props & RecurrenceProperties.ByMonth) != RecurrenceProperties.None)
			{
				stringBuilder.Append(";BYMONTH");
				stringBuilder.Append('=');
				this.OutputList(this.byMonth, stringBuilder);
			}
			if ((this.props & RecurrenceProperties.BySetPosition) != RecurrenceProperties.None)
			{
				stringBuilder.Append(";BYSETPOS");
				stringBuilder.Append('=');
				this.OutputList(this.bySetPos, stringBuilder);
			}
			if ((this.props & RecurrenceProperties.WeekStart) != RecurrenceProperties.None)
			{
				stringBuilder.Append(";WKST");
				stringBuilder.Append('=');
				stringBuilder.Append(Recurrence.GetDayOfWeekString(this.workWeekStart));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0002FD40 File Offset: 0x0002DF40
		private static Frequency GetFrequency(string s)
		{
			Frequency result;
			if (!Recurrence.frequencyToEnumTable.TryGetValue(s.ToUpper(), out result))
			{
				return Frequency.Unknown;
			}
			return result;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0002FD64 File Offset: 0x0002DF64
		private static RecurrenceProperties GetRecurProp(string s)
		{
			RecurrenceProperties result;
			if (!Recurrence.recurPropToEnumTable.TryGetValue(s.ToUpper(), out result))
			{
				return RecurrenceProperties.None;
			}
			return result;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0002FD88 File Offset: 0x0002DF88
		private static string GetDayOfWeekString(DayOfWeek d)
		{
			switch (d)
			{
			case DayOfWeek.Sunday:
				return "SU";
			case DayOfWeek.Monday:
				return "MO";
			case DayOfWeek.Tuesday:
				return "TU";
			case DayOfWeek.Wednesday:
				return "WE";
			case DayOfWeek.Thursday:
				return "TH";
			case DayOfWeek.Friday:
				return "FR";
			case DayOfWeek.Saturday:
				return "SA";
			default:
				return string.Empty;
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002FDEC File Offset: 0x0002DFEC
		private static string GetFrequencyString(Frequency f)
		{
			switch (f)
			{
			case Frequency.Secondly:
				return "SECONDLY";
			case Frequency.Minutely:
				return "MINUTELY";
			case Frequency.Hourly:
				return "HOURLY";
			case Frequency.Daily:
				return "DAILY";
			case Frequency.Weekly:
				return "WEEKLY";
			case Frequency.Monthly:
				return "MONTHLY";
			case Frequency.Yearly:
				return "YEARLY";
			default:
				return string.Empty;
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0002FE50 File Offset: 0x0002E050
		private DayOfWeek GetDayOfWeek(string s)
		{
			string key;
			switch (key = s.ToUpper())
			{
			case "SU":
				return DayOfWeek.Sunday;
			case "MO":
				return DayOfWeek.Monday;
			case "TU":
				return DayOfWeek.Tuesday;
			case "WE":
				return DayOfWeek.Wednesday;
			case "TH":
				return DayOfWeek.Thursday;
			case "FR":
				return DayOfWeek.Friday;
			case "SA":
				return DayOfWeek.Saturday;
			}
			this.SetComplianceStatus(CalendarStrings.UnknownDayOfWeek);
			return DayOfWeek.Sunday;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0002FF24 File Offset: 0x0002E124
		private void OutputList(int[] list, StringBuilder s)
		{
			int num = list.Length;
			if (num > 0)
			{
				s.Append(list[0]);
			}
			for (int i = 1; i < num; i++)
			{
				s.Append(',');
				s.Append(list[i].ToString());
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002FF6B File Offset: 0x0002E16B
		private void SetComplianceStatus(string message)
		{
			if (this.tracker == null)
			{
				throw new InvalidCalendarDataException(message);
			}
			this.tracker.SetComplianceStatus(ComplianceStatus.InvalidValueFormat, CalendarStrings.InvalidValueFormat);
		}

		// Token: 0x0400074E RID: 1870
		private static Dictionary<string, Frequency> frequencyToEnumTable = new Dictionary<string, Frequency>();

		// Token: 0x0400074F RID: 1871
		private static Dictionary<string, RecurrenceProperties> recurPropToEnumTable = new Dictionary<string, RecurrenceProperties>();

		// Token: 0x04000750 RID: 1872
		private Frequency freq;

		// Token: 0x04000751 RID: 1873
		private DateTime untilDate;

		// Token: 0x04000752 RID: 1874
		private DateTime untilDateTime;

		// Token: 0x04000753 RID: 1875
		private int count;

		// Token: 0x04000754 RID: 1876
		private int interval = 1;

		// Token: 0x04000755 RID: 1877
		private int[] bySecond;

		// Token: 0x04000756 RID: 1878
		private int[] byMinute;

		// Token: 0x04000757 RID: 1879
		private int[] byHour;

		// Token: 0x04000758 RID: 1880
		private Recurrence.ByDay[] byDay;

		// Token: 0x04000759 RID: 1881
		private int[] byMonthDay;

		// Token: 0x0400075A RID: 1882
		private int[] byYearDay;

		// Token: 0x0400075B RID: 1883
		private int[] byWeekNumber;

		// Token: 0x0400075C RID: 1884
		private int[] byMonth;

		// Token: 0x0400075D RID: 1885
		private int[] bySetPos;

		// Token: 0x0400075E RID: 1886
		private RecurrenceProperties props;

		// Token: 0x0400075F RID: 1887
		private DayOfWeek workWeekStart = DayOfWeek.Monday;

		// Token: 0x04000760 RID: 1888
		private ComplianceTracker tracker;

		// Token: 0x020000DC RID: 220
		private enum ParserStates
		{
			// Token: 0x04000762 RID: 1890
			Name,
			// Token: 0x04000763 RID: 1891
			Value
		}

		// Token: 0x020000DD RID: 221
		public struct ByDay
		{
			// Token: 0x060008AA RID: 2218 RVA: 0x0002FF91 File Offset: 0x0002E191
			public ByDay(DayOfWeek day, int occurrenceNumber)
			{
				this.day = day;
				this.occurrenceNumber = occurrenceNumber;
			}

			// Token: 0x17000287 RID: 647
			// (get) Token: 0x060008AB RID: 2219 RVA: 0x0002FFA1 File Offset: 0x0002E1A1
			// (set) Token: 0x060008AC RID: 2220 RVA: 0x0002FFA9 File Offset: 0x0002E1A9
			public int OccurrenceNumber
			{
				get
				{
					return this.occurrenceNumber;
				}
				set
				{
					this.occurrenceNumber = value;
				}
			}

			// Token: 0x17000288 RID: 648
			// (get) Token: 0x060008AD RID: 2221 RVA: 0x0002FFB2 File Offset: 0x0002E1B2
			// (set) Token: 0x060008AE RID: 2222 RVA: 0x0002FFBA File Offset: 0x0002E1BA
			public DayOfWeek Day
			{
				get
				{
					return this.day;
				}
				set
				{
					this.day = value;
				}
			}

			// Token: 0x060008AF RID: 2223 RVA: 0x0002FFC4 File Offset: 0x0002E1C4
			public override string ToString()
			{
				string dayOfWeekString = Recurrence.GetDayOfWeekString(this.day);
				if (this.occurrenceNumber != 0)
				{
					return this.occurrenceNumber.ToString() + dayOfWeekString;
				}
				return dayOfWeekString;
			}

			// Token: 0x04000764 RID: 1892
			private int occurrenceNumber;

			// Token: 0x04000765 RID: 1893
			private DayOfWeek day;
		}
	}
}
