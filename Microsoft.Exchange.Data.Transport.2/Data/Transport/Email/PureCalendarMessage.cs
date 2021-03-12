using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Encoders;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000F9 RID: 249
	internal class PureCalendarMessage : IBody, IRelayable
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x0001AB4A File Offset: 0x00018D4A
		internal PureCalendarMessage(PureMimeMessage mimeMessage, MimePart mimePart, Charset charset)
		{
			this.originalCharset = charset;
			this.targetCharset = charset;
			this.mimeMessage = mimeMessage;
			this.mimePart = mimePart;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x0001AB70 File Offset: 0x00018D70
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x0001AB9F File Offset: 0x00018D9F
		public string Subject
		{
			get
			{
				string text = this.properties[PropertyId.Summary] as string;
				if (string.IsNullOrEmpty(text))
				{
					text = string.Empty;
				}
				return text;
			}
			set
			{
				this.properties[PropertyId.Summary] = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0001ABAF File Offset: 0x00018DAF
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x0001ABB7 File Offset: 0x00018DB7
		public Charset TargetCharset
		{
			get
			{
				return this.targetCharset;
			}
			set
			{
				this.targetCharset = value;
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001ABC0 File Offset: 0x00018DC0
		BodyFormat IBody.GetBodyFormat()
		{
			BodyData bodyData = this.BodyData;
			if (bodyData == null)
			{
				return BodyFormat.None;
			}
			return bodyData.BodyFormat;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001ABE0 File Offset: 0x00018DE0
		string IBody.GetCharsetName()
		{
			BodyData bodyData = this.BodyData;
			return bodyData.CharsetName;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001ABFA File Offset: 0x00018DFA
		MimePart IBody.GetMimePart()
		{
			return this.mimePart;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001AC04 File Offset: 0x00018E04
		Stream IBody.GetContentReadStream()
		{
			BodyData bodyData = this.BodyData;
			Stream readStream = bodyData.GetReadStream();
			return bodyData.ConvertReadStreamFormat(readStream);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001AC28 File Offset: 0x00018E28
		bool IBody.TryGetContentReadStream(out Stream stream)
		{
			BodyData bodyData = this.BodyData;
			stream = bodyData.GetReadStream();
			stream = bodyData.ConvertReadStreamFormat(stream);
			return true;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001AC50 File Offset: 0x00018E50
		Stream IBody.GetContentWriteStream(Charset charset)
		{
			BodyData bodyData = this.BodyData;
			bodyData.ReleaseStorage();
			if (charset != null && charset != bodyData.Charset && bodyData.Charset != Charset.UTF8)
			{
				bodyData.SetFormat(BodyFormat.Text, InternalBodyFormat.Text, Charset.UTF8);
				this.targetCharset = Charset.UTF8;
				this.mimeMessage.SetBodyPartCharset(this.mimePart, Charset.UTF8);
			}
			Stream stream = new BodyContentWriteStream(this);
			return bodyData.ConvertWriteStreamFormat(stream, charset);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001ACC2 File Offset: 0x00018EC2
		void IBody.SetNewContent(DataStorage storage, long start, long end)
		{
			this.BodyData.SetStorage(storage, start, end);
			this.TouchBody();
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001ACD8 File Offset: 0x00018ED8
		bool IBody.ConversionNeeded(int[] validCodepages)
		{
			return false;
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x0001ACDB File Offset: 0x00018EDB
		public string MapiMessageClass
		{
			get
			{
				if (this.mapiMessageClass == null)
				{
					this.FindMapiMessageClass();
				}
				return this.mapiMessageClass;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x0001ACF1 File Offset: 0x00018EF1
		internal PureMimeMessage MimeMessage
		{
			[DebuggerStepThrough]
			get
			{
				return this.mimeMessage;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x0001ACFC File Offset: 0x00018EFC
		internal BodyData BodyData
		{
			get
			{
				if (this.properties.ContainsKey(PropertyId.Description))
				{
					return this.properties[PropertyId.Description] as BodyData;
				}
				string text = this.properties[PropertyId.Method] as string;
				if (!string.IsNullOrEmpty(text))
				{
					text = text.Trim();
					if ((text.Equals("COUNTER", StringComparison.OrdinalIgnoreCase) || text.Equals("REPLY", StringComparison.OrdinalIgnoreCase)) && this.properties.ContainsKey(PropertyId.Comment))
					{
						return this.properties[PropertyId.Comment] as BodyData;
					}
				}
				return null;
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001AD89 File Offset: 0x00018F89
		internal void TouchBody()
		{
			this.properties.Touch(PropertyId.Description);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001AD98 File Offset: 0x00018F98
		internal bool Load()
		{
			if (!this.mimePart.TryGetContentReadStream(out this.stream))
			{
				return false;
			}
			bool result;
			try
			{
				this.properties = new PureCalendarMessage.CalendarPropertyBag(this);
				using (CalendarReader calendarReader = new CalendarReader(new SuppressCloseStream(this.stream), this.originalCharset.Name, CalendarComplianceMode.Loose))
				{
					this.properties.Load(calendarReader, this.originalCharset);
					result = true;
				}
			}
			catch (ByteEncoderException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001AE28 File Offset: 0x00019028
		public void WriteTo(Stream destination)
		{
			this.stream.Position = 0L;
			using (CalendarReader calendarReader = new CalendarReader(new SuppressCloseStream(this.stream), this.originalCharset.Name, CalendarComplianceMode.Loose))
			{
				using (CalendarWriter calendarWriter = new CalendarWriter(new SuppressCloseStream(destination), this.targetCharset.Name))
				{
					calendarWriter.SetLooseMode();
					this.properties.Write(calendarReader, calendarWriter);
				}
			}
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001AEC0 File Offset: 0x000190C0
		private void Invalidate()
		{
			this.mimeMessage.InvalidateCalendarContent();
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001AED0 File Offset: 0x000190D0
		private void FindMapiMessageClass()
		{
			string text = this.properties[PropertyId.Method] as string;
			if (string.IsNullOrEmpty(text))
			{
				this.mapiMessageClass = "IPM.Note";
				return;
			}
			text = text.Trim();
			if (text.Equals("PUBLISH", StringComparison.OrdinalIgnoreCase) || text.Equals("REQUEST", StringComparison.OrdinalIgnoreCase))
			{
				this.mapiMessageClass = "IPM.Schedule.Meeting.Request";
				return;
			}
			if (text.Equals("REPLY", StringComparison.OrdinalIgnoreCase))
			{
				this.FindClassFromParticipationStatus();
				return;
			}
			if (text.Equals("CANCEL", StringComparison.OrdinalIgnoreCase))
			{
				this.mapiMessageClass = "IPM.Schedule.Meeting.Canceled";
				return;
			}
			this.mapiMessageClass = "IPM.Note";
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001AF6C File Offset: 0x0001916C
		private void FindClassFromParticipationStatus()
		{
			this.mapiMessageClass = "IPM.Schedule.Meeting.Resp.Tent";
			string text = this.properties[PropertyId.Attendee] as string;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (text.Equals("ACCEPTED", StringComparison.OrdinalIgnoreCase))
			{
				this.mapiMessageClass = "IPM.Schedule.Meeting.Resp.Pos";
				return;
			}
			if (text.Equals("DECLINED", StringComparison.OrdinalIgnoreCase))
			{
				this.mapiMessageClass = "IPM.Schedule.Meeting.Resp.Neg";
			}
		}

		// Token: 0x0400041E RID: 1054
		private PureCalendarMessage.CalendarPropertyBag properties;

		// Token: 0x0400041F RID: 1055
		private PureMimeMessage mimeMessage;

		// Token: 0x04000420 RID: 1056
		private MimePart mimePart;

		// Token: 0x04000421 RID: 1057
		private Stream stream;

		// Token: 0x04000422 RID: 1058
		private Charset originalCharset;

		// Token: 0x04000423 RID: 1059
		private Charset targetCharset;

		// Token: 0x04000424 RID: 1060
		private string mapiMessageClass;

		// Token: 0x020000FA RID: 250
		internal struct CalendarPropertyBag
		{
			// Token: 0x0600079D RID: 1949 RVA: 0x0001AFD4 File Offset: 0x000191D4
			internal CalendarPropertyBag(PureCalendarMessage calendarMessage)
			{
				this.calendarMessage = calendarMessage;
				this.properties = new Dictionary<PropertyId, object>(PureCalendarMessage.CalendarPropertyBag.CalendarPropertyIdComparer);
				this.dirty = new Dictionary<PropertyId, bool>(PureCalendarMessage.CalendarPropertyBag.CalendarPropertyIdComparer);
				foreach (PropertyId key in PureCalendarMessage.CalendarPropertyBag.MessageProperties)
				{
					this.dirty[key] = false;
				}
			}

			// Token: 0x0600079E RID: 1950 RVA: 0x0001B02D File Offset: 0x0001922D
			internal bool ContainsKey(PropertyId property)
			{
				return this.properties.ContainsKey(property);
			}

			// Token: 0x17000206 RID: 518
			internal object this[PropertyId id]
			{
				get
				{
					object result;
					if (!this.properties.TryGetValue(id, out result))
					{
						return null;
					}
					return result;
				}
				set
				{
					if (this[id] != value)
					{
						this.Invalidate();
						this.dirty[id] = true;
					}
					this.properties[id] = value;
				}
			}

			// Token: 0x060007A1 RID: 1953 RVA: 0x0001B088 File Offset: 0x00019288
			internal void Touch(PropertyId id)
			{
				this.Invalidate();
				this.dirty[id] = true;
			}

			// Token: 0x060007A2 RID: 1954 RVA: 0x0001B0A0 File Offset: 0x000192A0
			internal void Load(CalendarReader reader, Charset charset)
			{
				while (reader.ReadNextComponent())
				{
					if (ComponentId.VEvent == reader.ComponentId)
					{
						CalendarPropertyReader propertyReader = reader.PropertyReader;
						while (propertyReader.ReadNextProperty())
						{
							PropertyId propertyId = propertyReader.PropertyId;
							if (this.dirty.ContainsKey(propertyId) && !this.properties.ContainsKey(propertyId))
							{
								if (PropertyId.Description == propertyId || PropertyId.Comment == propertyId)
								{
									byte[] array = null;
									TemporaryDataStorage temporaryDataStorage = new TemporaryDataStorage();
									using (Stream stream = temporaryDataStorage.OpenWriteStream(true))
									{
										using (Stream valueReadStream = propertyReader.GetValueReadStream())
										{
											DataStorage.CopyStreamToStream(valueReadStream, stream, long.MaxValue, ref array);
										}
									}
									BodyData bodyData = new BodyData();
									bodyData.SetFormat(BodyFormat.Text, InternalBodyFormat.Text, charset);
									bodyData.SetStorage(temporaryDataStorage, 0L, long.MaxValue);
									temporaryDataStorage.Release();
									this.properties[propertyId] = bodyData;
								}
								else if (PropertyId.Attendee == propertyId)
								{
									CalendarParameterReader parameterReader = propertyReader.ParameterReader;
									while (parameterReader.ReadNextParameter())
									{
										if (parameterReader.ParameterId == ParameterId.ParticipationStatus)
										{
											this.properties[propertyId] = parameterReader.ReadValue();
										}
									}
								}
								else
								{
									this.properties[propertyId] = propertyReader.ReadValue();
								}
							}
						}
					}
					if (ComponentId.VCalendar == reader.ComponentId)
					{
						CalendarPropertyReader propertyReader2 = reader.PropertyReader;
						while (propertyReader2.ReadNextProperty())
						{
							PropertyId propertyId2 = propertyReader2.PropertyId;
							if (PropertyId.Method == propertyId2)
							{
								this.properties[propertyId2] = propertyReader2.ReadValue();
							}
						}
					}
				}
			}

			// Token: 0x060007A3 RID: 1955 RVA: 0x0001B248 File Offset: 0x00019448
			internal void Write(CalendarReader reader, CalendarWriter writer)
			{
				int num = 0;
				while (reader.ReadNextComponent())
				{
					while (num-- >= reader.Depth)
					{
						writer.EndComponent();
					}
					writer.StartComponent(reader.ComponentName);
					this.WriteProperties(reader, writer);
					num = reader.Depth;
				}
				while (num-- > 0)
				{
					writer.EndComponent();
				}
			}

			// Token: 0x060007A4 RID: 1956 RVA: 0x0001B2A0 File Offset: 0x000194A0
			private void WriteProperties(CalendarReader reader, CalendarWriter writer)
			{
				CalendarPropertyReader propertyReader = reader.PropertyReader;
				while (propertyReader.ReadNextProperty())
				{
					PropertyId propertyId = propertyReader.PropertyId;
					bool flag;
					if (ComponentId.VEvent == reader.ComponentId && this.dirty.TryGetValue(propertyId, out flag) && flag)
					{
						object obj = this[propertyId];
						if (obj != null)
						{
							this.WriteProperty(writer, propertyId, obj);
						}
					}
					else
					{
						writer.WriteProperty(propertyReader);
					}
				}
			}

			// Token: 0x060007A5 RID: 1957 RVA: 0x0001B300 File Offset: 0x00019500
			private void WriteProperty(CalendarWriter writer, PropertyId id, object value)
			{
				BodyData bodyData = value as BodyData;
				if (bodyData != null)
				{
					using (Stream readStream = bodyData.GetReadStream())
					{
						using (StreamReader streamReader = new StreamReader(readStream, bodyData.Encoding))
						{
							string value2 = streamReader.ReadToEnd();
							writer.WriteProperty(id, value2);
						}
						return;
					}
				}
				string value3 = value as string;
				if (!string.IsNullOrEmpty(value3))
				{
					writer.WriteProperty(id, value3);
				}
			}

			// Token: 0x060007A6 RID: 1958 RVA: 0x0001B388 File Offset: 0x00019588
			private void Invalidate()
			{
				this.calendarMessage.Invalidate();
			}

			// Token: 0x04000425 RID: 1061
			private static readonly PropertyId[] MessageProperties = new PropertyId[]
			{
				PropertyId.Method,
				PropertyId.Description,
				PropertyId.Summary,
				PropertyId.Attendee,
				PropertyId.Comment
			};

			// Token: 0x04000426 RID: 1062
			private static readonly PureCalendarMessage.CalendarPropertyBag.PropertyIdComparer CalendarPropertyIdComparer = new PureCalendarMessage.CalendarPropertyBag.PropertyIdComparer();

			// Token: 0x04000427 RID: 1063
			private Dictionary<PropertyId, object> properties;

			// Token: 0x04000428 RID: 1064
			private Dictionary<PropertyId, bool> dirty;

			// Token: 0x04000429 RID: 1065
			private PureCalendarMessage calendarMessage;

			// Token: 0x020000FB RID: 251
			private class PropertyIdComparer : IEqualityComparer<PropertyId>
			{
				// Token: 0x060007A8 RID: 1960 RVA: 0x0001B3D3 File Offset: 0x000195D3
				public bool Equals(PropertyId x, PropertyId y)
				{
					return x == y;
				}

				// Token: 0x060007A9 RID: 1961 RVA: 0x0001B3D9 File Offset: 0x000195D9
				public int GetHashCode(PropertyId obj)
				{
					return (int)obj;
				}
			}
		}

		// Token: 0x020000FC RID: 252
		private static class CalendarMethod
		{
			// Token: 0x0400042A RID: 1066
			public const string Publish = "PUBLISH";

			// Token: 0x0400042B RID: 1067
			public const string Request = "REQUEST";

			// Token: 0x0400042C RID: 1068
			public const string Reply = "REPLY";

			// Token: 0x0400042D RID: 1069
			public const string Cancel = "CANCEL";

			// Token: 0x0400042E RID: 1070
			public const string Refresh = "REFRESH";

			// Token: 0x0400042F RID: 1071
			public const string Counter = "COUNTER";
		}

		// Token: 0x020000FD RID: 253
		private static class CalendarStatus
		{
			// Token: 0x04000430 RID: 1072
			public const string NeedsAction = "NEEDS-ACTION";

			// Token: 0x04000431 RID: 1073
			public const string Accepted = "ACCEPTED";

			// Token: 0x04000432 RID: 1074
			public const string Declined = "DECLINED";

			// Token: 0x04000433 RID: 1075
			public const string Tentative = "TENTATIVE";

			// Token: 0x04000434 RID: 1076
			public const string Delegated = "DELEGATED";
		}
	}
}
