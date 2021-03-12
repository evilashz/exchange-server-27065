using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000214 RID: 532
	public class SmtpProbeWorkDefinition
	{
		// Token: 0x06001093 RID: 4243 RVA: 0x00030308 File Offset: 0x0002E508
		public SmtpProbeWorkDefinition(int id, ProbeDefinition probeDefinition) : this(id, probeDefinition, null)
		{
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00030313 File Offset: 0x0002E513
		public SmtpProbeWorkDefinition(int id, ProbeDefinition probeDefinition, DelTraceDebug traceDebug)
		{
			this.workItemId = id;
			this.probeDefinition = probeDefinition;
			this.traceDebug = traceDebug;
			this.LoadFromContext(probeDefinition.ExtensionAttributes);
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x0003033C File Offset: 0x0002E53C
		// (set) Token: 0x06001096 RID: 4246 RVA: 0x00030344 File Offset: 0x0002E544
		public SmtpProbeWorkDefinition.SendMailDefinition SendMail { get; internal set; }

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x0003034D File Offset: 0x0002E54D
		// (set) Token: 0x06001098 RID: 4248 RVA: 0x00030355 File Offset: 0x0002E555
		public SmtpProbeWorkDefinition.CheckMailDefinition CheckMail { get; internal set; }

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x0003035E File Offset: 0x0002E55E
		// (set) Token: 0x0600109A RID: 4250 RVA: 0x00030366 File Offset: 0x0002E566
		public List<Notification> ExpectedNotifications { get; internal set; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0003036F File Offset: 0x0002E56F
		// (set) Token: 0x0600109C RID: 4252 RVA: 0x00030377 File Offset: 0x0002E577
		public bool ClientCertificateValid
		{
			get
			{
				return this.clientCertificateValid;
			}
			internal set
			{
				this.clientCertificateValid = value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x00030380 File Offset: 0x0002E580
		// (set) Token: 0x0600109E RID: 4254 RVA: 0x00030388 File Offset: 0x0002E588
		public ClientCertificateCriteria ClientCertificate { get; internal set; }

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x00030391 File Offset: 0x0002E591
		// (set) Token: 0x060010A0 RID: 4256 RVA: 0x00030399 File Offset: 0x0002E599
		public SmtpProbeWorkDefinition.TargetDataDefinition TargetData { get; internal set; }

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x000303A4 File Offset: 0x0002E5A4
		internal bool IsMailflowProbe
		{
			get
			{
				return this.probeDefinition.TypeName == typeof(SmtpProbe).FullName || this.probeDefinition.TypeName == typeof(BucketedSmtpProbe).FullName;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x000303F3 File Offset: 0x0002E5F3
		// (set) Token: 0x060010A3 RID: 4259 RVA: 0x000303FB File Offset: 0x0002E5FB
		internal bool IsUnitTest { get; set; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x00030404 File Offset: 0x0002E604
		// (set) Token: 0x060010A5 RID: 4261 RVA: 0x0003040C File Offset: 0x0002E60C
		internal bool IsCortex { get; private set; }

		// Token: 0x060010A6 RID: 4262 RVA: 0x00030418 File Offset: 0x0002E618
		private void LoadFromContext(string xml)
		{
			if (string.IsNullOrWhiteSpace(xml))
			{
				throw new ArgumentException("Work Definition XML is null");
			}
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.LoadXml(xml);
			if (xmlDocument.HasChildNodes && xmlDocument.FirstChild.Name == "ExtensionAttributes")
			{
				XmlDocument xmlDocument2 = new SafeXmlDocument();
				xmlDocument2.LoadXml(xmlDocument.FirstChild.InnerXml.ToString());
				xmlDocument = xmlDocument2;
			}
			XmlNode xmlNode = xmlDocument.SelectSingleNode("//WorkContext");
			if (xmlNode != null)
			{
				this.IsUnitTest = (Utils.GetOptionalXmlAttribute<bool>(xmlNode, "IsUnitTest", false) && ExEnvironment.IsTest);
				this.IsCortex = Utils.GetOptionalXmlAttribute<bool>(xmlNode, "IsCortex", false);
			}
			this.SendMail = SmtpProbeWorkDefinition.SendMailDefinition.FromXml(Utils.CheckXmlElement(xmlDocument.SelectSingleNode("//SendMail"), "SendMail"), this.IsMailflowProbe);
			this.SendMail.Message = Message.FromXml(this.workItemId, xmlDocument, this.probeDefinition, new DelTraceDebug(this.TraceDebug));
			this.ClientCertificate = ClientCertificateCriteria.FromXml(xmlDocument.SelectSingleNode("//ClientCertificate"), out this.clientCertificateValid);
			this.TargetData = SmtpProbeWorkDefinition.TargetDataDefinition.FromXml(xmlDocument.SelectSingleNode("//TargetData"));
			if (this.IsMailflowProbe)
			{
				this.CheckMail = SmtpProbeWorkDefinition.CheckMailDefinition.FromXml(Utils.CheckXmlElement(xmlDocument.SelectSingleNode("//CheckMail"), "CheckMail"), this.probeDefinition.RecurrenceIntervalSeconds, this.SendMail.Sla, this.SendMail.Enabled, new DelTraceDebug(this.TraceDebug));
				return;
			}
			XmlElement xmlElement = Utils.CheckXmlElement(xmlDocument.SelectSingleNode("//Match"), "Match");
			List<Notification> list = new List<Notification>();
			foreach (object obj in xmlElement.SelectNodes("Notification"))
			{
				XmlNode definition = (XmlNode)obj;
				list.Add(new Notification
				{
					Type = Utils.GetMandatoryXmlAttribute<string>(definition, "Type"),
					Value = Utils.GetMandatoryXmlAttribute<string>(definition, "Value"),
					Category = Utils.GetOptionalXmlAttribute<string>(definition, "Category", string.Empty),
					Mandatory = Utils.GetOptionalXmlAttribute<bool>(definition, "Mandatory", false),
					MatchExpected = Utils.GetOptionalXmlAttribute<bool>(definition, "MatchExpected", true),
					Method = Utils.GetOptionalXmlEnumAttribute<MatchType>(definition, "MatchType", MatchType.String)
				});
			}
			if (list.Count == 0)
			{
				throw new ArgumentException("Work definition error - Number of Notification nodes = 0.");
			}
			this.ExpectedNotifications = list;
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x000306B4 File Offset: 0x0002E8B4
		private void TraceDebug(string format, params object[] args)
		{
			if (this.traceDebug != null)
			{
				this.traceDebug(format, args);
				return;
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.SMTPTracer, new TracingContext(), format, args, null, "TraceDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpProbeWorkDefinition.cs", 250);
		}

		// Token: 0x040007E7 RID: 2023
		private readonly int workItemId;

		// Token: 0x040007E8 RID: 2024
		private readonly ProbeDefinition probeDefinition;

		// Token: 0x040007E9 RID: 2025
		private readonly DelTraceDebug traceDebug;

		// Token: 0x040007EA RID: 2026
		private bool clientCertificateValid;

		// Token: 0x02000215 RID: 533
		public class CheckMailDefinition
		{
			// Token: 0x17000500 RID: 1280
			// (get) Token: 0x060010A9 RID: 4265 RVA: 0x000306F5 File Offset: 0x0002E8F5
			// (set) Token: 0x060010AA RID: 4266 RVA: 0x000306FD File Offset: 0x0002E8FD
			public bool Enabled { get; internal set; }

			// Token: 0x17000501 RID: 1281
			// (get) Token: 0x060010AB RID: 4267 RVA: 0x00030706 File Offset: 0x0002E906
			// (set) Token: 0x060010AC RID: 4268 RVA: 0x0003070E File Offset: 0x0002E90E
			public string PopServer { get; internal set; }

			// Token: 0x17000502 RID: 1282
			// (get) Token: 0x060010AD RID: 4269 RVA: 0x00030717 File Offset: 0x0002E917
			// (set) Token: 0x060010AE RID: 4270 RVA: 0x0003071F File Offset: 0x0002E91F
			public int Port { get; internal set; }

			// Token: 0x17000503 RID: 1283
			// (get) Token: 0x060010AF RID: 4271 RVA: 0x00030728 File Offset: 0x0002E928
			// (set) Token: 0x060010B0 RID: 4272 RVA: 0x00030730 File Offset: 0x0002E930
			public bool EnableSsl { get; internal set; }

			// Token: 0x17000504 RID: 1284
			// (get) Token: 0x060010B1 RID: 4273 RVA: 0x00030739 File Offset: 0x0002E939
			// (set) Token: 0x060010B2 RID: 4274 RVA: 0x00030741 File Offset: 0x0002E941
			public int ReadTimeout { get; internal set; }

			// Token: 0x17000505 RID: 1285
			// (get) Token: 0x060010B3 RID: 4275 RVA: 0x0003074A File Offset: 0x0002E94A
			// (set) Token: 0x060010B4 RID: 4276 RVA: 0x00030752 File Offset: 0x0002E952
			public string Username { get; internal set; }

			// Token: 0x17000506 RID: 1286
			// (get) Token: 0x060010B5 RID: 4277 RVA: 0x0003075B File Offset: 0x0002E95B
			// (set) Token: 0x060010B6 RID: 4278 RVA: 0x00030763 File Offset: 0x0002E963
			public string Password { get; internal set; }

			// Token: 0x17000507 RID: 1287
			// (get) Token: 0x060010B7 RID: 4279 RVA: 0x0003076C File Offset: 0x0002E96C
			// (set) Token: 0x060010B8 RID: 4280 RVA: 0x00030774 File Offset: 0x0002E974
			public int QueryTimeWindow { get; internal set; }

			// Token: 0x17000508 RID: 1288
			// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0003077D File Offset: 0x0002E97D
			// (set) Token: 0x060010BA RID: 4282 RVA: 0x00030785 File Offset: 0x0002E985
			public int DeleteMessageMinutes { get; internal set; }

			// Token: 0x17000509 RID: 1289
			// (get) Token: 0x060010BB RID: 4283 RVA: 0x0003078E File Offset: 0x0002E98E
			// (set) Token: 0x060010BC RID: 4284 RVA: 0x00030796 File Offset: 0x0002E996
			public ExpectedMessage ExpectedMessage { get; internal set; }

			// Token: 0x1700050A RID: 1290
			// (get) Token: 0x060010BD RID: 4285 RVA: 0x0003079F File Offset: 0x0002E99F
			// (set) Token: 0x060010BE RID: 4286 RVA: 0x000307A7 File Offset: 0x0002E9A7
			public string LogFileLocation { get; internal set; }

			// Token: 0x1700050B RID: 1291
			// (get) Token: 0x060010BF RID: 4287 RVA: 0x000307B0 File Offset: 0x0002E9B0
			// (set) Token: 0x060010C0 RID: 4288 RVA: 0x000307B8 File Offset: 0x0002E9B8
			public bool DisableCheckMailByMessageId { get; internal set; }

			// Token: 0x1700050C RID: 1292
			// (get) Token: 0x060010C1 RID: 4289 RVA: 0x000307C1 File Offset: 0x0002E9C1
			// (set) Token: 0x060010C2 RID: 4290 RVA: 0x000307C9 File Offset: 0x0002E9C9
			public bool VerifyDeliverResultBeforeDelete { get; internal set; }

			// Token: 0x060010C3 RID: 4291 RVA: 0x000307D4 File Offset: 0x0002E9D4
			public static SmtpProbeWorkDefinition.CheckMailDefinition FromXml(XmlElement workContext, int recurrenceIntervalSeconds, double sla, bool sendEnabled, DelTraceDebug traceDebug)
			{
				SmtpProbeWorkDefinition.CheckMailDefinition checkMailDefinition = new SmtpProbeWorkDefinition.CheckMailDefinition();
				checkMailDefinition.Enabled = Utils.GetBoolean(workContext.GetAttribute("Enabled"), "Enabled", true);
				if (checkMailDefinition.Enabled)
				{
					checkMailDefinition.PopServer = Utils.CheckNullOrWhiteSpace(workContext.GetAttribute("PopServerUri"), "PopServerUri");
					checkMailDefinition.Port = Utils.GetInteger(workContext.GetAttribute("Port"), "Port", 995, 1);
					checkMailDefinition.EnableSsl = Utils.GetBoolean(workContext.GetAttribute("EnableSsl"), "EnableSsl", true);
					checkMailDefinition.ReadTimeout = Utils.GetInteger(workContext.GetAttribute("ReadTimeout"), "ReadTimeout", 120, 0);
					checkMailDefinition.Username = Utils.CheckNullOrWhiteSpace(workContext.GetAttribute("Username"), "Username");
					checkMailDefinition.Password = Utils.CheckNullOrWhiteSpace(workContext.GetAttribute("Password"), "Password");
					checkMailDefinition.QueryTimeWindow = Utils.GetInteger(workContext.GetAttribute("CheckMailQueryWindow"), "CheckMailQueryWindow", 900, 0);
					checkMailDefinition.DeleteMessageMinutes = Utils.GetInteger(workContext.GetAttribute("DeleteMessageMinutes"), "DeleteMessageMinutes", 120, 0);
					checkMailDefinition.LogFileLocation = workContext.GetAttribute("LogFileLocation");
					checkMailDefinition.DisableCheckMailByMessageId = (!sendEnabled && Utils.GetOptionalXmlAttribute<bool>(workContext, "DisableCheckMailByMessageId", false));
					checkMailDefinition.VerifyDeliverResultBeforeDelete = Utils.GetOptionalXmlAttribute<bool>(workContext, "VerifyDeliverResultBeforeDelete", false);
					if (!string.IsNullOrWhiteSpace(checkMailDefinition.LogFileLocation))
					{
						Directory.GetAccessControl(checkMailDefinition.LogFileLocation);
						traceDebug("LogFileLocation={0}", new object[]
						{
							checkMailDefinition.LogFileLocation
						});
					}
					double val = Math.Max(sla * 60.0, (double)recurrenceIntervalSeconds) * (ExEnvironment.IsTest ? 2.0 : 4.0);
					int num = (int)Math.Min(val, 2147483647.0);
					if (!Utils.GetOptionalXmlAttribute<bool>(workContext, "EnforceWindow", false) && checkMailDefinition.QueryTimeWindow < num)
					{
						checkMailDefinition.QueryTimeWindow = num;
						traceDebug("QueryWindow changed to {0}s", new object[]
						{
							num
						});
					}
					else
					{
						traceDebug("QueryWindow={0}s.", new object[]
						{
							checkMailDefinition.QueryTimeWindow
						});
					}
					if (checkMailDefinition.DeleteMessageMinutes != 0 && TimeSpan.FromMinutes((double)checkMailDefinition.DeleteMessageMinutes).TotalSeconds < (double)checkMailDefinition.QueryTimeWindow)
					{
						checkMailDefinition.DeleteMessageMinutes = (int)Math.Ceiling(TimeSpan.FromSeconds((double)checkMailDefinition.QueryTimeWindow).TotalMinutes);
					}
					XmlNode xmlNode = workContext.SelectSingleNode("Match");
					if (xmlNode != null)
					{
						ExpectedMessage expectedMessage = new ExpectedMessage();
						XmlNode xmlNode2 = xmlNode.SelectSingleNode("Subject");
						if (xmlNode2 != null)
						{
							Notification notification = new Notification();
							notification.Type = "Subject";
							string mandatoryXmlAttribute = Utils.GetMandatoryXmlAttribute<string>(xmlNode2, "Value");
							notification.Value = ((mandatoryXmlAttribute == null) ? mandatoryXmlAttribute : mandatoryXmlAttribute.Trim());
							notification.Mandatory = Utils.GetOptionalXmlAttribute<bool>(xmlNode2, "Mandatory", true);
							notification.Method = Utils.GetOptionalXmlEnumAttribute<MatchType>(xmlNode2, "MatchType", MatchType.String);
							notification.MatchExpected = Utils.GetOptionalXmlAttribute<bool>(xmlNode2, "MatchExpected", true);
							expectedMessage.Subject = notification;
							traceDebug("MatchSubject: '{0}'", new object[]
							{
								notification.Value
							});
						}
						xmlNode2 = xmlNode.SelectSingleNode("Body");
						if (xmlNode2 != null)
						{
							Notification notification2 = new Notification();
							notification2.Type = "Body";
							string mandatoryXmlAttribute2 = Utils.GetMandatoryXmlAttribute<string>(xmlNode2, "Value");
							notification2.Value = ((mandatoryXmlAttribute2 == null) ? mandatoryXmlAttribute2 : mandatoryXmlAttribute2.Trim());
							notification2.Mandatory = Utils.GetOptionalXmlAttribute<bool>(xmlNode2, "Mandatory", true);
							notification2.Method = Utils.GetOptionalXmlEnumAttribute<MatchType>(xmlNode2, "MatchType", MatchType.String);
							notification2.MatchExpected = Utils.GetOptionalXmlAttribute<bool>(xmlNode2, "MatchExpected", true);
							expectedMessage.Body = notification2;
							traceDebug("MatchBody: '{0}'", new object[]
							{
								notification2.Value
							});
						}
						foreach (object obj in xmlNode.SelectNodes("Header"))
						{
							XmlNode definition = (XmlNode)obj;
							Notification notification3 = new Notification();
							notification3.Type = Utils.GetMandatoryXmlAttribute<string>(definition, "Tag");
							notification3.Value = Utils.GetMandatoryXmlAttribute<string>(definition, "Value");
							notification3.Mandatory = Utils.GetOptionalXmlAttribute<bool>(definition, "Mandatory", true);
							notification3.Method = Utils.GetOptionalXmlEnumAttribute<MatchType>(definition, "MatchType", MatchType.String);
							notification3.MatchExpected = Utils.GetOptionalXmlAttribute<bool>(definition, "MatchExpected", true);
							expectedMessage.Headers.Add(notification3);
							traceDebug("MatchHeader: {0}='{1}'", new object[]
							{
								notification3.Type,
								notification3.Value
							});
						}
						xmlNode2 = xmlNode.SelectSingleNode("Attachment");
						if (xmlNode2 != null)
						{
							Notification notification4 = new Notification();
							notification4.Type = "Attachment";
							int optionalXmlAttribute = Utils.GetOptionalXmlAttribute<int>(xmlNode2, "Value", 0);
							notification4.Value = ((optionalXmlAttribute < 0) ? 0 : optionalXmlAttribute).ToString();
							notification4.Mandatory = Utils.GetOptionalXmlAttribute<bool>(xmlNode2, "Mandatory", true);
							notification4.Method = Utils.GetOptionalXmlEnumAttribute<MatchType>(xmlNode2, "MatchType", MatchType.String);
							notification4.MatchExpected = Utils.GetOptionalXmlAttribute<bool>(xmlNode2, "MatchExpected", true);
							expectedMessage.Attachment = notification4;
							traceDebug("MatchAttachment#: '{0}'", new object[]
							{
								notification4.Value
							});
						}
						if (expectedMessage.Subject != null || expectedMessage.Body != null || expectedMessage.Headers != null || expectedMessage.Attachment != null)
						{
							checkMailDefinition.ExpectedMessage = expectedMessage;
						}
					}
				}
				return checkMailDefinition;
			}
		}

		// Token: 0x02000216 RID: 534
		public class SendMailDefinition
		{
			// Token: 0x1700050D RID: 1293
			// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00030DC4 File Offset: 0x0002EFC4
			// (set) Token: 0x060010C6 RID: 4294 RVA: 0x00030DCC File Offset: 0x0002EFCC
			public bool ResolveEndPoint { get; internal set; }

			// Token: 0x1700050E RID: 1294
			// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00030DD5 File Offset: 0x0002EFD5
			// (set) Token: 0x060010C8 RID: 4296 RVA: 0x00030DDD File Offset: 0x0002EFDD
			public string SmtpServer { get; internal set; }

			// Token: 0x1700050F RID: 1295
			// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00030DE6 File Offset: 0x0002EFE6
			// (set) Token: 0x060010CA RID: 4298 RVA: 0x00030DEE File Offset: 0x0002EFEE
			public string OriginalFQDN { get; internal set; }

			// Token: 0x17000510 RID: 1296
			// (get) Token: 0x060010CB RID: 4299 RVA: 0x00030DF7 File Offset: 0x0002EFF7
			// (set) Token: 0x060010CC RID: 4300 RVA: 0x00030DFF File Offset: 0x0002EFFF
			public double Sla { get; internal set; }

			// Token: 0x17000511 RID: 1297
			// (get) Token: 0x060010CD RID: 4301 RVA: 0x00030E08 File Offset: 0x0002F008
			// (set) Token: 0x060010CE RID: 4302 RVA: 0x00030E10 File Offset: 0x0002F010
			public int Port { get; internal set; }

			// Token: 0x17000512 RID: 1298
			// (get) Token: 0x060010CF RID: 4303 RVA: 0x00030E19 File Offset: 0x0002F019
			// (set) Token: 0x060010D0 RID: 4304 RVA: 0x00030E21 File Offset: 0x0002F021
			public bool EnableSsl { get; internal set; }

			// Token: 0x17000513 RID: 1299
			// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00030E2A File Offset: 0x0002F02A
			// (set) Token: 0x060010D2 RID: 4306 RVA: 0x00030E32 File Offset: 0x0002F032
			public bool RequireTLS { get; internal set; }

			// Token: 0x17000514 RID: 1300
			// (get) Token: 0x060010D3 RID: 4307 RVA: 0x00030E3B File Offset: 0x0002F03B
			// (set) Token: 0x060010D4 RID: 4308 RVA: 0x00030E43 File Offset: 0x0002F043
			public bool Anonymous { get; internal set; }

			// Token: 0x17000515 RID: 1301
			// (get) Token: 0x060010D5 RID: 4309 RVA: 0x00030E4C File Offset: 0x0002F04C
			// (set) Token: 0x060010D6 RID: 4310 RVA: 0x00030E54 File Offset: 0x0002F054
			public int Timeout { get; internal set; }

			// Token: 0x17000516 RID: 1302
			// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00030E5D File Offset: 0x0002F05D
			// (set) Token: 0x060010D8 RID: 4312 RVA: 0x00030E65 File Offset: 0x0002F065
			public string SenderUsername { get; internal set; }

			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x060010D9 RID: 4313 RVA: 0x00030E6E File Offset: 0x0002F06E
			// (set) Token: 0x060010DA RID: 4314 RVA: 0x00030E76 File Offset: 0x0002F076
			public string SenderPassword { get; internal set; }

			// Token: 0x17000518 RID: 1304
			// (get) Token: 0x060010DB RID: 4315 RVA: 0x00030E7F File Offset: 0x0002F07F
			// (set) Token: 0x060010DC RID: 4316 RVA: 0x00030E87 File Offset: 0x0002F087
			public string RecipientUsername { get; internal set; }

			// Token: 0x17000519 RID: 1305
			// (get) Token: 0x060010DD RID: 4317 RVA: 0x00030E90 File Offset: 0x0002F090
			// (set) Token: 0x060010DE RID: 4318 RVA: 0x00030E98 File Offset: 0x0002F098
			public Message Message { get; internal set; }

			// Token: 0x1700051A RID: 1306
			// (get) Token: 0x060010DF RID: 4319 RVA: 0x00030EA1 File Offset: 0x0002F0A1
			// (set) Token: 0x060010E0 RID: 4320 RVA: 0x00030EA9 File Offset: 0x0002F0A9
			public bool Enabled { get; internal set; }

			// Token: 0x1700051B RID: 1307
			// (get) Token: 0x060010E1 RID: 4321 RVA: 0x00030EB2 File Offset: 0x0002F0B2
			// (set) Token: 0x060010E2 RID: 4322 RVA: 0x00030EBA File Offset: 0x0002F0BA
			public string SenderTenantID { get; internal set; }

			// Token: 0x1700051C RID: 1308
			// (get) Token: 0x060010E3 RID: 4323 RVA: 0x00030EC3 File Offset: 0x0002F0C3
			// (set) Token: 0x060010E4 RID: 4324 RVA: 0x00030ECB File Offset: 0x0002F0CB
			public string RecipientTenantID { get; internal set; }

			// Token: 0x1700051D RID: 1309
			// (get) Token: 0x060010E5 RID: 4325 RVA: 0x00030ED4 File Offset: 0x0002F0D4
			// (set) Token: 0x060010E6 RID: 4326 RVA: 0x00030EDC File Offset: 0x0002F0DC
			public Directionality Direction { get; internal set; }

			// Token: 0x1700051E RID: 1310
			// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00030EE5 File Offset: 0x0002F0E5
			// (set) Token: 0x060010E8 RID: 4328 RVA: 0x00030EED File Offset: 0x0002F0ED
			public bool IgnoreSendMailFailure { get; internal set; }

			// Token: 0x1700051F RID: 1311
			// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00030EF6 File Offset: 0x0002F0F6
			// (set) Token: 0x060010EA RID: 4330 RVA: 0x00030EFE File Offset: 0x0002F0FE
			public bool IgnoreCertificateNameMismatchPolicyError { get; internal set; }

			// Token: 0x17000520 RID: 1312
			// (get) Token: 0x060010EB RID: 4331 RVA: 0x00030F07 File Offset: 0x0002F107
			// (set) Token: 0x060010EC RID: 4332 RVA: 0x00030F0F File Offset: 0x0002F10F
			public DnsUtils.DnsRecordType RecordResolveType { get; internal set; }

			// Token: 0x17000521 RID: 1313
			// (get) Token: 0x060010ED RID: 4333 RVA: 0x00030F18 File Offset: 0x0002F118
			// (set) Token: 0x060010EE RID: 4334 RVA: 0x00030F20 File Offset: 0x0002F120
			public bool SimpleResolution { get; internal set; }

			// Token: 0x17000522 RID: 1314
			// (get) Token: 0x060010EF RID: 4335 RVA: 0x00030F29 File Offset: 0x0002F129
			// (set) Token: 0x060010F0 RID: 4336 RVA: 0x00030F31 File Offset: 0x0002F131
			public bool AuthOnly { get; internal set; }

			// Token: 0x17000523 RID: 1315
			// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00030F3A File Offset: 0x0002F13A
			// (set) Token: 0x060010F2 RID: 4338 RVA: 0x00030F42 File Offset: 0x0002F142
			public bool RcptOnly { get; internal set; }

			// Token: 0x17000524 RID: 1316
			// (get) Token: 0x060010F3 RID: 4339 RVA: 0x00030F4B File Offset: 0x0002F14B
			// (set) Token: 0x060010F4 RID: 4340 RVA: 0x00030F53 File Offset: 0x0002F153
			public AddressFamily IpVersion { get; internal set; }

			// Token: 0x17000525 RID: 1317
			// (get) Token: 0x060010F5 RID: 4341 RVA: 0x00030F5C File Offset: 0x0002F15C
			// (set) Token: 0x060010F6 RID: 4342 RVA: 0x00030F64 File Offset: 0x0002F164
			public int ConnectRetryCount { get; internal set; }

			// Token: 0x060010F7 RID: 4343 RVA: 0x00030F70 File Offset: 0x0002F170
			public static SmtpProbeWorkDefinition.SendMailDefinition FromXml(XmlElement workContext, bool isMailflowProbe)
			{
				SmtpProbeWorkDefinition.SendMailDefinition sendMailDefinition = new SmtpProbeWorkDefinition.SendMailDefinition();
				sendMailDefinition.Enabled = Utils.GetBoolean(workContext.GetAttribute("Enabled"), "Enabled", true);
				sendMailDefinition.OriginalFQDN = Utils.CheckNullOrWhiteSpace(workContext.GetAttribute("SmtpServerUri"), "SmtpServerUri");
				sendMailDefinition.SmtpServer = sendMailDefinition.OriginalFQDN;
				sendMailDefinition.ResolveEndPoint = Utils.GetBoolean(workContext.GetAttribute("ResolveEndPoint"), "ResolveEndPoint", false);
				sendMailDefinition.Sla = Utils.GetTimeSpan(workContext.GetAttribute("SLA"), "SLA", TimeSpan.Parse("00:01:00")).TotalMinutes;
				sendMailDefinition.Port = Utils.GetInteger(workContext.GetAttribute("Port"), "Port", 25, 1);
				sendMailDefinition.EnableSsl = Utils.GetBoolean(workContext.GetAttribute("EnableSsl"), "EnableSsl", true);
				sendMailDefinition.RequireTLS = Utils.GetBoolean(workContext.GetAttribute("RequireTLS"), "RequireTLS", false);
				sendMailDefinition.Timeout = Utils.GetInteger(workContext.GetAttribute("Timeout"), "Timeout", 0, 0);
				sendMailDefinition.Anonymous = Utils.GetBoolean(workContext.GetAttribute("Anonymous"), "Anonymous", false);
				sendMailDefinition.SenderTenantID = Utils.GetOptionalXmlAttribute<string>(workContext, "SenderTenantID", string.Empty);
				sendMailDefinition.RecipientTenantID = Utils.GetOptionalXmlAttribute<string>(workContext, "RecipientTenantID", string.Empty);
				sendMailDefinition.Direction = Utils.GetOptionalXmlEnumAttribute<Directionality>(workContext, "Direction", Directionality.Incoming);
				sendMailDefinition.IgnoreCertificateNameMismatchPolicyError = Utils.GetOptionalXmlAttribute<bool>(workContext, "IgnoreCertificateNameMismatchPolicyError", false);
				sendMailDefinition.AuthOnly = Utils.GetOptionalXmlAttribute<bool>(workContext, "AuthOnly", false);
				sendMailDefinition.RcptOnly = Utils.GetOptionalXmlAttribute<bool>(workContext, "RcptOnly", false);
				sendMailDefinition.SimpleResolution = Utils.GetOptionalXmlAttribute<bool>(workContext, "SimpleResolution", true);
				sendMailDefinition.IgnoreSendMailFailure = Utils.GetOptionalXmlAttribute<bool>(workContext, "IgnoreSendMailFailure", !isMailflowProbe);
				sendMailDefinition.ConnectRetryCount = Utils.GetOptionalXmlAttribute<int>(workContext, "ConnectRetryCount", 0);
				sendMailDefinition.RecordResolveType = Utils.GetOptionalXmlEnumAttribute<DnsUtils.DnsRecordType>(workContext, "RecordResolveType", DnsUtils.DnsRecordType.DnsTypeMX);
				sendMailDefinition.IpVersion = SmtpProbeWorkDefinition.SendMailDefinition.GetIPAddressFamily(sendMailDefinition);
				XmlElement xmlElement = Utils.CheckXmlElement(workContext.SelectSingleNode("MailFrom"), "MailFrom");
				sendMailDefinition.SenderUsername = Utils.CheckNullOrWhiteSpace(xmlElement.GetAttribute("Username"), "Username");
				sendMailDefinition.SenderPassword = xmlElement.GetAttribute("Password");
				XmlElement xmlElement2 = Utils.CheckXmlElement(workContext.SelectSingleNode("MailTo"), "MailTo");
				sendMailDefinition.RecipientUsername = Utils.CheckNullOrWhiteSpace(xmlElement2.GetAttribute("Username"), "Username");
				return sendMailDefinition;
			}

			// Token: 0x060010F8 RID: 4344 RVA: 0x000311E4 File Offset: 0x0002F3E4
			private static AddressFamily GetIPAddressFamily(SmtpProbeWorkDefinition.SendMailDefinition sd)
			{
				IPAddress ipaddress;
				if (sd.ResolveEndPoint)
				{
					if (sd.RecordResolveType == DnsUtils.DnsRecordType.DnsTypeAAAA)
					{
						return AddressFamily.InterNetworkV6;
					}
					if (sd.RecordResolveType == DnsUtils.DnsRecordType.DnsTypeA || sd.RecordResolveType == DnsUtils.DnsRecordType.DnsTypeMX)
					{
						return AddressFamily.InterNetwork;
					}
				}
				else if (IPAddress.TryParse(sd.SmtpServer, out ipaddress))
				{
					return ipaddress.AddressFamily;
				}
				return AddressFamily.Unknown;
			}
		}

		// Token: 0x02000217 RID: 535
		public class TargetDataDefinition
		{
			// Token: 0x17000526 RID: 1318
			// (get) Token: 0x060010FA RID: 4346 RVA: 0x0003123A File Offset: 0x0002F43A
			// (set) Token: 0x060010FB RID: 4347 RVA: 0x00031242 File Offset: 0x0002F442
			public string MailboxDatabase { get; internal set; }

			// Token: 0x17000527 RID: 1319
			// (get) Token: 0x060010FC RID: 4348 RVA: 0x0003124B File Offset: 0x0002F44B
			// (set) Token: 0x060010FD RID: 4349 RVA: 0x00031253 File Offset: 0x0002F453
			public string MailboxDatabaseVersion { get; internal set; }

			// Token: 0x060010FE RID: 4350 RVA: 0x0003125C File Offset: 0x0002F45C
			public static SmtpProbeWorkDefinition.TargetDataDefinition FromXml(XmlNode workContext)
			{
				XmlElement xmlElement = workContext as XmlElement;
				SmtpProbeWorkDefinition.TargetDataDefinition targetDataDefinition = null;
				if (xmlElement != null)
				{
					targetDataDefinition = new SmtpProbeWorkDefinition.TargetDataDefinition();
					XmlNode xmlNode = workContext.SelectSingleNode("MailboxDatabase");
					if (xmlNode != null)
					{
						targetDataDefinition.MailboxDatabase = Utils.CheckNullOrWhiteSpace(xmlNode.InnerText, "TargetData MailboxDatabase");
					}
					xmlNode = workContext.SelectSingleNode("MailboxDatabaseVersion");
					if (xmlNode != null)
					{
						targetDataDefinition.MailboxDatabaseVersion = Utils.CheckNullOrWhiteSpace(xmlNode.InnerText, "TargetData MailboxDatabaseVersion");
					}
				}
				return targetDataDefinition;
			}
		}
	}
}
