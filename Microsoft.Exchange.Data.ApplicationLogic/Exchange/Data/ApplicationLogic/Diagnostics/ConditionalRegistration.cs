using System;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000D1 RID: 209
	internal class ConditionalRegistration : BaseConditionalRegistration
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x00023480 File Offset: 0x00021680
		public static ConditionalRegistration CreateFromArgument(DiagnosableParameters argument)
		{
			if (BaseConditionalRegistration.FetchSchema == null || BaseConditionalRegistration.QuerySchema == null || string.IsNullOrEmpty(ConditionalRegistrationLog.ProtocolName))
			{
				throw new InvalidOperationException("Can not use Conditional Diagnostics Handlers without proper initialization. Call 'BaseConditionalRegistration.Initialize' to initialize pre-requisites.");
			}
			string propertiesToFetch;
			string whereClause;
			BaseConditionalRegistration.ParseArgument(argument.Argument, out propertiesToFetch, out whereClause);
			string arg;
			uint num;
			TimeSpan timeSpan;
			ConditionalRegistration.ParseOptions(argument.Argument, out arg, out num, out timeSpan);
			return new ConditionalRegistration(string.Format("[{0}] {1}", argument.UserIdentity, arg), argument.UserIdentity.Replace("/", "-"), Guid.NewGuid().ToString(), propertiesToFetch, whereClause, (int)((num == 0U) ? 10U : num), (timeSpan <= TimeSpan.Zero) ? ConditionalRegistration.DefaultTimeToLive : timeSpan);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00023538 File Offset: 0x00021738
		public static ConditionalRegistration DeserializeFromStreamReader(StreamReader registrationStream)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(registrationStream.ReadToEnd());
			TimeSpan value = TimeSpan.Parse(xmlDocument.SelectSingleNode("/ConditionalRegistration/TimeToLive").InnerText);
			ExDateTime t = ExDateTime.Parse(xmlDocument.SelectSingleNode("/ConditionalRegistration/CreatedTime").InnerText).Add(value);
			if (ExDateTime.UtcNow < t)
			{
				return new ConditionalRegistration(xmlDocument.SelectSingleNode("/ConditionalRegistration/Description").InnerText, xmlDocument.SelectSingleNode("/ConditionalRegistration/User").InnerText, xmlDocument.SelectSingleNode("/ConditionalRegistration/Cookie").InnerText, xmlDocument.SelectSingleNode("/ConditionalRegistration/PropertiesToFetch").InnerText, xmlDocument.SelectSingleNode("/ConditionalRegistration/Filter").InnerText, int.Parse(xmlDocument.SelectSingleNode("/ConditionalRegistration/MaxHits").InnerText), t.Subtract(ExDateTime.UtcNow));
			}
			return null;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00023614 File Offset: 0x00021814
		private static void ParseOptions(string argument, out string description, out uint maxHits, out TimeSpan timeToLive)
		{
			string text = argument.Trim();
			string text2 = text.ToLower();
			maxHits = 0U;
			timeToLive = TimeSpan.Zero;
			description = string.Empty;
			int num = text2.IndexOf(" options ");
			if (num != -1)
			{
				string text3 = text.Substring(num + " options ".Length);
				string[] array = text3.Split(new char[]
				{
					','
				});
				foreach (string text4 in array)
				{
					string text5 = text4.Trim().ToLower();
					if (text5.StartsWith("maxhits", StringComparison.OrdinalIgnoreCase))
					{
						string rightHand = BaseConditionalRegistration.GetRightHand(text5);
						if (!uint.TryParse(rightHand, out maxHits))
						{
							throw new ArgumentException("Invalid value for MaxHits: " + rightHand);
						}
					}
					else if (text5.StartsWith("timetolive", StringComparison.OrdinalIgnoreCase))
					{
						string rightHand2 = BaseConditionalRegistration.GetRightHand(text5);
						if (!TimeSpan.TryParse(rightHand2, out timeToLive))
						{
							throw new ArgumentException("Invalid value for TimeToLive: " + rightHand2);
						}
						if (timeToLive <= TimeSpan.Zero)
						{
							throw new ArgumentOutOfRangeException("TimeToLive must be > TimeSpan.Zero.  Actual: " + timeToLive);
						}
					}
					else
					{
						if (!text5.StartsWith("description", StringComparison.OrdinalIgnoreCase))
						{
							throw new ArgumentException(string.Format("Unknown option: '{0}'", text5));
						}
						description = BaseConditionalRegistration.GetRightHand(text5);
					}
				}
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00023778 File Offset: 0x00021978
		public ConditionalRegistration(string description, string user, string cookie, string propertiesToFetch, string whereClause, TimeSpan timeToLive) : this(description, user, cookie, propertiesToFetch, whereClause, 10, timeToLive)
		{
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002378B File Offset: 0x0002198B
		public ConditionalRegistration(string description, string user, string cookie, string propertiesToFetch, string whereClause, int maxHits, TimeSpan timeToLive) : base(cookie, user, propertiesToFetch, whereClause)
		{
			this.Description = description;
			this.MaxHits = maxHits;
			this.TimeToLive = timeToLive;
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x000237B0 File Offset: 0x000219B0
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x000237B8 File Offset: 0x000219B8
		public int MaxHits { get; private set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x000237C1 File Offset: 0x000219C1
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x000237C9 File Offset: 0x000219C9
		public override string Description
		{
			get
			{
				return this.description;
			}
			protected set
			{
				this.description = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x000237D2 File Offset: 0x000219D2
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x000237DA File Offset: 0x000219DA
		public TimeSpan TimeToLive { get; private set; }

		// Token: 0x0400042A RID: 1066
		private const string MaxHitsKeyword = "maxhits";

		// Token: 0x0400042B RID: 1067
		private const string TimeToLiveKeyword = "timetolive";

		// Token: 0x0400042C RID: 1068
		private const string DescriptionKeyword = "description";

		// Token: 0x0400042D RID: 1069
		private const int MaxPropertiesToFetch = 100;

		// Token: 0x0400042E RID: 1070
		internal const int DefaultMaxHits = 10;

		// Token: 0x0400042F RID: 1071
		private static readonly TimeSpan DefaultTimeToLive = TimeSpan.FromHours(1.0);

		// Token: 0x04000430 RID: 1072
		private string description;
	}
}
