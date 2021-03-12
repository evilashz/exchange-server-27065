using System;
using System.Globalization;
using System.Net;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000169 RID: 361
	[Serializable]
	internal class AirSyncTaskStateProperty : AirSyncProperty, ITaskState, IProperty
	{
		// Token: 0x06001035 RID: 4149 RVA: 0x0005B9E9 File Offset: 0x00059BE9
		public AirSyncTaskStateProperty(string xmlNodeNamespace, string airSyncCompleteTagName, string airSyncDateCompletedTag, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncCompleteTagName, airSyncDateCompletedTag, requiresClientSupport)
		{
			this.completeTagName = airSyncCompleteTagName;
			this.dateCompletedTag = airSyncDateCompletedTag;
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0005BA04 File Offset: 0x00059C04
		public bool Complete
		{
			get
			{
				string innerText;
				if ((innerText = base.XmlNode.InnerText) != null)
				{
					if (innerText == "0")
					{
						return false;
					}
					if (innerText == "1")
					{
						return true;
					}
				}
				throw new ConversionException("Incorrectly formatted boolean");
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0005BA4C File Offset: 0x00059C4C
		public ExDateTime? DateCompleted
		{
			get
			{
				XmlNode xmlNode = base.XmlNode.ParentNode[this.dateCompletedTag];
				if (xmlNode == null)
				{
					return null;
				}
				ExDateTime value;
				if (!ExDateTime.TryParseExact(xmlNode.InnerText, "yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out value))
				{
					throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidDateTime, null, false)
					{
						ErrorStringForProtocolLogger = "InvalidDateTimeInAirSyncTaskState"
					};
				}
				return new ExDateTime?(value);
			}
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0005BABC File Offset: 0x00059CBC
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			ITaskState taskState = srcProperty as ITaskState;
			if (taskState == null)
			{
				throw new UnexpectedTypeException("ITaskState", srcProperty);
			}
			if (taskState.Complete)
			{
				base.CreateAirSyncNode(this.completeTagName, "1");
				if (taskState.DateCompleted != null)
				{
					base.CreateAirSyncNode(this.dateCompletedTag, taskState.DateCompleted.Value.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo));
					return;
				}
			}
			else
			{
				base.CreateAirSyncNode(this.completeTagName, "0");
			}
		}

		// Token: 0x04000A84 RID: 2692
		private string completeTagName;

		// Token: 0x04000A85 RID: 2693
		private string dateCompletedTag;
	}
}
