using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200014C RID: 332
	[Serializable]
	internal class AirSyncExceptionsProperty : AirSyncProperty, IExceptionsProperty, IMultivaluedProperty<ExceptionInstance>, IProperty, IEnumerable<ExceptionInstance>, IEnumerable, IDataObjectGeneratorContainer
	{
		// Token: 0x06000FD6 RID: 4054 RVA: 0x00059EE4 File Offset: 0x000580E4
		public AirSyncExceptionsProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00059EFA File Offset: 0x000580FA
		public int Count
		{
			get
			{
				return base.XmlNode.ChildNodes.Count;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00059F0C File Offset: 0x0005810C
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x00059F14 File Offset: 0x00058114
		public IDataObjectGenerator SchemaState
		{
			get
			{
				return this.schemaState;
			}
			set
			{
				this.schemaState = (value as IAirSyncDataObjectGenerator);
			}
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0005A298 File Offset: 0x00058498
		public IEnumerator<ExceptionInstance> GetEnumerator()
		{
			foreach (object obj in base.XmlNode.ChildNodes)
			{
				XmlNode exceptionNode = (XmlNode)obj;
				XmlNamespaceManager mgr = new XmlNamespaceManager(base.XmlNode.OwnerDocument.NameTable);
				mgr.AddNamespace("X", exceptionNode.NamespaceURI);
				XmlNode startTimeNode = exceptionNode.SelectSingleNode("X:ExceptionStartTime", mgr);
				ExDateTime startTime;
				if (!ExDateTime.TryParseExact(startTimeNode.InnerText, "yyyyMMdd\\THHmmss\\Z", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out startTime))
				{
					throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidDateTime, null, false)
					{
						ErrorStringForProtocolLogger = "InvalidDateTimeInAirSyncException"
					};
				}
				XmlNode deletedNode = exceptionNode.SelectSingleNode("X:Deleted", mgr);
				if (deletedNode != null && deletedNode.InnerText == "1")
				{
					ExceptionInstance returnVal = new ExceptionInstance(startTime, 1);
					yield return returnVal;
				}
				else
				{
					exceptionNode.RemoveChild(startTimeNode);
					if (deletedNode != null)
					{
						exceptionNode.RemoveChild(deletedNode);
					}
					AirSyncDataObject exceptionContainer = this.schemaState.GetInnerAirSyncDataObject(this.missingPropertyStrategy);
					try
					{
						exceptionContainer.Bind(exceptionNode);
						yield return new ExceptionInstance(startTime, 0)
						{
							ModifiedException = exceptionContainer
						};
						exceptionNode.AppendChild(startTimeNode);
						if (deletedNode != null)
						{
							exceptionNode.AppendChild(deletedNode);
						}
					}
					finally
					{
						exceptionContainer.Unbind();
					}
				}
			}
			yield break;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0005A2B4 File Offset: 0x000584B4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0005A2CC File Offset: 0x000584CC
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IExceptionsProperty exceptionsProperty = srcProperty as IExceptionsProperty;
			if (exceptionsProperty == null)
			{
				throw new UnexpectedTypeException("IExceptionsProperty", srcProperty);
			}
			base.XmlNode = base.XmlParentNode.OwnerDocument.CreateElement(base.AirSyncTagNames[0], base.Namespace);
			foreach (ExceptionInstance exceptionInstance in exceptionsProperty)
			{
				XmlNode xmlNode = base.XmlParentNode.OwnerDocument.CreateElement("Exception", base.Namespace);
				AirSyncDataObject innerAirSyncDataObject = this.schemaState.GetInnerAirSyncDataObject(this.missingPropertyStrategy);
				try
				{
					innerAirSyncDataObject.Bind(xmlNode);
					if (exceptionInstance.Deleted == 1)
					{
						XmlNode xmlNode2 = base.XmlParentNode.OwnerDocument.CreateElement("Deleted", base.Namespace);
						xmlNode2.InnerText = "1";
						xmlNode.AppendChild(xmlNode2);
						XmlNode xmlNode3 = base.XmlParentNode.OwnerDocument.CreateElement("ExceptionStartTime", base.Namespace);
						xmlNode3.InnerText = exceptionInstance.ExceptionStartTime.ToString("yyyyMMdd\\THHmmss\\Z", DateTimeFormatInfo.InvariantInfo);
						xmlNode.AppendChild(xmlNode3);
					}
					else
					{
						innerAirSyncDataObject.CopyFrom(exceptionInstance.ModifiedException);
					}
				}
				finally
				{
					innerAirSyncDataObject.Unbind();
				}
				base.XmlNode.AppendChild(xmlNode);
			}
			if (base.XmlNode.HasChildNodes)
			{
				base.XmlParentNode.AppendChild(base.XmlNode);
			}
		}

		// Token: 0x04000A77 RID: 2679
		private const string Format = "yyyyMMdd\\THHmmss\\Z";

		// Token: 0x04000A78 RID: 2680
		private IAirSyncMissingPropertyStrategy missingPropertyStrategy = new AirSyncSetToUnmodifiedStrategy();

		// Token: 0x04000A79 RID: 2681
		private IAirSyncDataObjectGenerator schemaState;
	}
}
