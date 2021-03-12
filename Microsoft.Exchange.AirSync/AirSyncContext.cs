using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000016 RID: 22
	internal class AirSyncContext : IAirSyncContext, IReadOnlyPropertyBag, INotificationManagerContext, ISyncLogger
	{
		// Token: 0x06000117 RID: 279 RVA: 0x0000A2DC File Offset: 0x000084DC
		internal AirSyncContext(HttpContext httpContext)
		{
			this.httpContext = httpContext;
			this.request = new AirSyncRequest(this, httpContext.Request);
			this.response = new AirSyncResponse(this, httpContext.Response);
			if (ConditionalRegistrationCache.Singleton.PropertyIsActive(AirSyncConditionalHandlerSchema.PerCallTracing))
			{
				this.perCallTracer = new StringBuilder();
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000A358 File Offset: 0x00008558
		public Participant GetFullParticipant(string legDN)
		{
			Participant result;
			if (this.cachedParticipants.TryGetValue(legDN, out result))
			{
				this.participantCacheHits++;
				return result;
			}
			this.participantCacheMisses++;
			return null;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000A394 File Offset: 0x00008594
		public void CacheParticipant(string legDN, Participant fullParticipant)
		{
			this.cachedParticipants.TryAdd(legDN, fullParticipant);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000A3A4 File Offset: 0x000085A4
		public string GetParticipantCacheData()
		{
			return string.Format("H:{0}, M:{1}", this.participantCacheHits, this.participantCacheMisses);
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000A3C6 File Offset: 0x000085C6
		public bool PerCallTracingEnabled
		{
			get
			{
				return this.perCallTracer != null;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000A3D4 File Offset: 0x000085D4
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0000A3DC File Offset: 0x000085DC
		IAirSyncUser IAirSyncContext.User
		{
			get
			{
				return this.user;
			}
			set
			{
				this.user = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000A3E5 File Offset: 0x000085E5
		string IAirSyncContext.TaskDescription
		{
			get
			{
				return string.Format("{0}:{1}", ((IAirSyncContext)this).Request.CommandType, (this.user == null) ? "<no user>" : this.user.Name);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000A41B File Offset: 0x0000861B
		// (set) Token: 0x06000120 RID: 288 RVA: 0x0000A423 File Offset: 0x00008623
		TimeTracker IAirSyncContext.Tracker { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000A42C File Offset: 0x0000862C
		// (set) Token: 0x06000122 RID: 290 RVA: 0x0000A439 File Offset: 0x00008639
		IPrincipal IAirSyncContext.Principal
		{
			get
			{
				return this.httpContext.User;
			}
			set
			{
				this.httpContext.User = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000A448 File Offset: 0x00008648
		string IAirSyncContext.WindowsLiveId
		{
			get
			{
				string text = this.httpContext.GetMemberName();
				if (string.IsNullOrEmpty(text))
				{
					text = this.httpContext.Request.Headers["X-WLID-MemberName"];
				}
				return text;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000A488 File Offset: 0x00008688
		Dictionary<EasFeature, bool> IAirSyncContext.FlightingOverrides
		{
			get
			{
				if (this.flightingOverrides == null)
				{
					if (GlobalSettings.AllowFlightingOverrides)
					{
						string text = this.httpContext.Request.Headers["X-EAS-FlightingOverrides"];
						if (!string.IsNullOrEmpty(text))
						{
							string[] array = text.Split(new char[]
							{
								','
							});
							foreach (string text2 in array)
							{
								string[] array3 = text2.Split(new char[]
								{
									'='
								});
								EasFeature key;
								bool value;
								if (array3.Length == 2 && Enum.TryParse<EasFeature>(array3[0], out key) && bool.TryParse(array3[1], out value))
								{
									if (this.flightingOverrides == null)
									{
										this.flightingOverrides = new Dictionary<EasFeature, bool>();
									}
									this.flightingOverrides[key] = value;
								}
							}
						}
					}
					if (this.flightingOverrides == null)
					{
						this.flightingOverrides = AirSyncContext.EmptyOverrides;
					}
				}
				return this.flightingOverrides;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000A573 File Offset: 0x00008773
		IAirSyncRequest IAirSyncContext.Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000A57B File Offset: 0x0000877B
		IAirSyncResponse IAirSyncContext.Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000A583 File Offset: 0x00008783
		ProtocolLogger IAirSyncContext.ProtocolLogger
		{
			get
			{
				return this.protocolLogger;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000A58B File Offset: 0x0000878B
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000A593 File Offset: 0x00008793
		DeviceBehavior IAirSyncContext.DeviceBehavior { get; set; }

		// Token: 0x0600012A RID: 298 RVA: 0x0000A59C File Offset: 0x0000879C
		public void Information(Trace tracer, long id, string message)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.InfoTrace, message, new object[0]);
			}
			tracer.Information(id, message);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000A5BC File Offset: 0x000087BC
		public void Information(Trace tracer, long id, string formatString, params object[] args)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.InfoTrace, formatString, args);
			}
			tracer.Information(id, formatString, args);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000A5DC File Offset: 0x000087DC
		public void Information<T0>(Trace tracer, long id, string formatString, T0 arg0)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.InfoTrace, formatString, new object[]
				{
					arg0
				});
			}
			tracer.Information<T0>(id, formatString, arg0);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000A618 File Offset: 0x00008818
		public void Information<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.InfoTrace, formatString, new object[]
				{
					arg0,
					arg1
				});
			}
			tracer.Information<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000A660 File Offset: 0x00008860
		public void Information<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.InfoTrace, formatString, new object[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			tracer.Information<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000A6B1 File Offset: 0x000088B1
		public void TraceDebug(Trace tracer, long id, string message)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.DebugTrace, message, new object[0]);
			}
			tracer.TraceDebug(id, message);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000A6D1 File Offset: 0x000088D1
		public void TraceDebug(Trace tracer, int lid, long id, string message)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.DebugTrace, message, new object[0]);
			}
			tracer.TraceDebug(lid, id, message);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000A6F4 File Offset: 0x000088F4
		public void TraceDebug(Trace tracer, long id, string formatString, params object[] args)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.DebugTrace, formatString, args);
			}
			tracer.TraceDebug(id, formatString, args);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000A714 File Offset: 0x00008914
		public void TraceDebug<T0>(Trace tracer, long id, string formatString, T0 arg0)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.DebugTrace, formatString, new object[]
				{
					arg0
				});
			}
			tracer.TraceDebug<T0>(id, formatString, arg0);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000A74D File Offset: 0x0000894D
		public void TraceDebug(Trace tracer, int lid, long id, string formatString, params object[] args)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.DebugTrace, formatString, args);
			}
			tracer.TraceDebug(lid, id, formatString, args);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000A770 File Offset: 0x00008970
		public void TraceDebug<T0>(Trace tracer, int lid, long id, string formatString, T0 arg0)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.DebugTrace, formatString, new object[]
				{
					arg0
				});
			}
			tracer.TraceDebug<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000A7AC File Offset: 0x000089AC
		public void TraceDebug<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.DebugTrace, formatString, new object[]
				{
					arg0,
					arg1
				});
			}
			tracer.TraceDebug<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000A7F4 File Offset: 0x000089F4
		public void TraceDebug<T0, T1>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.DebugTrace, formatString, new object[]
				{
					arg0,
					arg1
				});
			}
			tracer.TraceDebug<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000A83C File Offset: 0x00008A3C
		public void TraceDebug<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.DebugTrace, formatString, new object[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			tracer.TraceDebug<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000A890 File Offset: 0x00008A90
		public void TraceDebug<T0, T1, T2>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.DebugTrace, formatString, new object[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			tracer.TraceDebug<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000A8E4 File Offset: 0x00008AE4
		public void TraceError(Trace tracer, long id, string message)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.ErrorTrace, message, new object[0]);
			}
			tracer.TraceError(id, message);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000A904 File Offset: 0x00008B04
		public void TraceError(Trace tracer, int lid, long id, string message)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.ErrorTrace, message, new object[0]);
			}
			tracer.TraceError(lid, id, message);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000A927 File Offset: 0x00008B27
		public void TraceError(Trace tracer, long id, string formatString, params object[] args)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.ErrorTrace, formatString, args);
			}
			tracer.TraceError(id, formatString, args);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000A948 File Offset: 0x00008B48
		public void TraceError<T0>(Trace tracer, long id, string formatString, T0 arg0)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.ErrorTrace, formatString, new object[]
				{
					arg0
				});
			}
			tracer.TraceError<T0>(id, formatString, arg0);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000A981 File Offset: 0x00008B81
		public void TraceError(Trace tracer, int lid, long id, string formatString, params object[] args)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.ErrorTrace, formatString, args);
			}
			tracer.TraceError(lid, id, formatString, args);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000A9A4 File Offset: 0x00008BA4
		public void TraceError<T0>(Trace tracer, int lid, long id, string formatString, T0 arg0)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.ErrorTrace, formatString, new object[]
				{
					arg0
				});
			}
			tracer.TraceError<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		public void TraceError<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.ErrorTrace, formatString, new object[]
				{
					arg0,
					arg1
				});
			}
			tracer.TraceError<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000AA28 File Offset: 0x00008C28
		public void TraceError<T0, T1>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.ErrorTrace, formatString, new object[]
				{
					arg0,
					arg1
				});
			}
			tracer.TraceError<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000AA70 File Offset: 0x00008C70
		public void TraceError<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.ErrorTrace, formatString, new object[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			tracer.TraceError<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		public void TraceError<T0, T1, T2>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.ErrorTrace, formatString, new object[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			tracer.TraceError<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000AB18 File Offset: 0x00008D18
		public void TraceFunction(Trace tracer, long id, string message)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.FunctionTrace, message, new object[0]);
			}
			tracer.TraceFunction(id, message);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000AB38 File Offset: 0x00008D38
		public void TraceFunction(Trace tracer, int lid, long id, string message)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.FunctionTrace, message, new object[0]);
			}
			tracer.TraceFunction(lid, id, message);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000AB5B File Offset: 0x00008D5B
		public void TraceFunction(Trace tracer, long id, string formatString, params object[] args)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.FunctionTrace, formatString, args);
			}
			tracer.TraceFunction(id, formatString, args);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000AB7C File Offset: 0x00008D7C
		public void TraceFunction<T0>(Trace tracer, long id, string formatString, T0 arg0)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.FunctionTrace, formatString, new object[]
				{
					arg0
				});
			}
			tracer.TraceFunction<T0>(id, formatString, arg0);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000ABB5 File Offset: 0x00008DB5
		public void TraceFunction(Trace tracer, int lid, long id, string formatString, params object[] args)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.FunctionTrace, formatString, args);
			}
			tracer.TraceFunction(lid, id, formatString, args);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000ABD8 File Offset: 0x00008DD8
		public void TraceFunction<T0>(Trace tracer, int lid, long id, string formatString, T0 arg0)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.FunctionTrace, formatString, new object[]
				{
					arg0
				});
			}
			tracer.TraceFunction<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000AC14 File Offset: 0x00008E14
		public void TraceFunction<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.FunctionTrace, formatString, new object[]
				{
					arg0,
					arg1
				});
			}
			tracer.TraceFunction<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000AC5C File Offset: 0x00008E5C
		public void TraceFunction<T0, T1>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.FunctionTrace, formatString, new object[]
				{
					arg0,
					arg1
				});
			}
			tracer.TraceFunction<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000ACA4 File Offset: 0x00008EA4
		public void TraceFunction<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.FunctionTrace, formatString, new object[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			tracer.TraceFunction<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		public void TraceFunction<T0, T1, T2>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.FunctionTrace, formatString, new object[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			tracer.TraceFunction<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public void TracePfd(Trace tracer, long id, string message)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.PfdTrace, message, new object[0]);
			}
			tracer.TracePfd(id, message);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000AD6C File Offset: 0x00008F6C
		public void TracePfd(Trace tracer, int lid, long id, string message)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.PfdTrace, message, new object[0]);
			}
			tracer.TracePfd(lid, id, message);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000AD8F File Offset: 0x00008F8F
		public void TracePfd(Trace tracer, long id, string formatString, params object[] args)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.PfdTrace, formatString, args);
			}
			tracer.TracePfd(id, formatString, args);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		public void TracePfd<T0>(Trace tracer, long id, string formatString, T0 arg0)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.PfdTrace, formatString, new object[]
				{
					arg0
				});
			}
			tracer.TracePfd<T0>(id, formatString, arg0);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000ADE9 File Offset: 0x00008FE9
		public void TracePfd(Trace tracer, int lid, long id, string formatString, params object[] args)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.PfdTrace, formatString, args);
			}
			tracer.TracePfd(lid, id, formatString, args);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000AE0C File Offset: 0x0000900C
		public void TracePfd<T0>(Trace tracer, int lid, long id, string formatString, T0 arg0)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.PfdTrace, formatString, new object[]
				{
					arg0
				});
			}
			tracer.TracePfd<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000AE48 File Offset: 0x00009048
		public void TracePfd<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.PfdTrace, formatString, new object[]
				{
					arg0,
					arg1
				});
			}
			tracer.TracePfd<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000AE90 File Offset: 0x00009090
		public void TracePfd<T0, T1>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.PfdTrace, formatString, new object[]
				{
					arg0,
					arg1
				});
			}
			tracer.TracePfd<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000AED8 File Offset: 0x000090D8
		public void TracePfd<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.PfdTrace, formatString, new object[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			tracer.TracePfd<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000AF2C File Offset: 0x0000912C
		public void TracePfd<T0, T1, T2>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.PfdTrace, formatString, new object[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			tracer.TracePfd<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000AF80 File Offset: 0x00009180
		public void TraceWarning(Trace tracer, long id, string message)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.WarningTrace, message, new object[0]);
			}
			tracer.TraceWarning(id, message);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000AFA0 File Offset: 0x000091A0
		public void TraceWarning(Trace tracer, int lid, long id, string message)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.WarningTrace, message, new object[0]);
			}
			tracer.TraceWarning(lid, id, message);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000AFC3 File Offset: 0x000091C3
		public void TraceWarning(Trace tracer, long id, string formatString, params object[] args)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.WarningTrace, formatString, args);
			}
			tracer.TraceWarning(id, formatString, args);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000AFE4 File Offset: 0x000091E4
		public void TraceWarning<T0>(Trace tracer, long id, string formatString, T0 arg0)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.WarningTrace, formatString, new object[]
				{
					arg0
				});
			}
			tracer.TraceWarning<T0>(id, formatString, arg0);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000B01D File Offset: 0x0000921D
		public void TraceWarning(Trace tracer, int lid, long id, string formatString, params object[] args)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.WarningTrace, formatString, args);
			}
			tracer.TraceWarning(lid, id, formatString, args);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000B040 File Offset: 0x00009240
		public void TraceWarning<T0>(Trace tracer, int lid, long id, string formatString, T0 arg0)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.WarningTrace, formatString, new object[]
				{
					arg0
				});
			}
			tracer.TraceWarning<T0>(lid, id, formatString, arg0);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000B07C File Offset: 0x0000927C
		public void TraceWarning<T0, T1>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.WarningTrace, formatString, new object[]
				{
					arg0,
					arg1
				});
			}
			tracer.TraceWarning<T0, T1>(id, formatString, arg0, arg1);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000B0C4 File Offset: 0x000092C4
		public void TraceWarning<T0, T1>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.WarningTrace, formatString, new object[]
				{
					arg0,
					arg1
				});
			}
			tracer.TraceWarning<T0, T1>(lid, id, formatString, arg0, arg1);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000B10C File Offset: 0x0000930C
		public void TraceWarning<T0, T1, T2>(Trace tracer, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.WarningTrace, formatString, new object[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			tracer.TraceWarning<T0, T1, T2>(id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000B160 File Offset: 0x00009360
		public void TraceWarning<T0, T1, T2>(Trace tracer, int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (this.PerCallTracingEnabled)
			{
				this.PerCallTrace(TraceType.WarningTrace, formatString, new object[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			tracer.TraceWarning<T0, T1, T2>(lid, id, formatString, arg0, arg1, arg2);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000B1B4 File Offset: 0x000093B4
		private string GetLogPrefix(TraceType traceType)
		{
			return string.Format("[{0}/{1}/{2}] ", DateTime.UtcNow, Thread.CurrentThread.ManagedThreadId, ((AirSyncContext.EasTraceType)traceType).ToString());
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000B1E4 File Offset: 0x000093E4
		private void PerCallTrace(TraceType traceType, string formatString, params object[] args)
		{
			if (this.PerCallTracingEnabled)
			{
				string logPrefix = this.GetLogPrefix(traceType);
				if (args != null && args.Length > 0)
				{
					this.perCallTracer.AppendLine(logPrefix + string.Format(formatString, args));
					return;
				}
				this.perCallTracer.AppendLine(logPrefix + formatString);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000B237 File Offset: 0x00009437
		void IAirSyncContext.PrepareToHang()
		{
			this.user.PrepareToHang();
			((IAirSyncContext)this).Request.PrepareToHang();
			((IAirSyncContext)this).ProtocolLogger.SetValueIfNotSet(ProtocolLoggerData.TimeHang, ExDateTime.UtcNow);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000B264 File Offset: 0x00009464
		void IAirSyncContext.WriteActivityContextData()
		{
			string activityContextData = ((IAirSyncContext)this).GetActivityContextData();
			if (!string.IsNullOrEmpty(activityContextData))
			{
				this.protocolLogger.SetValue(ProtocolLoggerData.ActivityContextData, activityContextData);
			}
			if (GlobalSettings.WriteActivityContextDiagnostics)
			{
				((IAirSyncContext)this).Response.AppendHeader("X-ActivityContextDiagnostics", string.IsNullOrEmpty(activityContextData) ? "<empty>" : activityContextData);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000B2B8 File Offset: 0x000094B8
		string IAirSyncContext.GetActivityContextData()
		{
			return "ActivityID=" + ((INotificationManagerContext)this).ActivityScope.ActivityId.ToString() + ";" + LogRowFormatter.FormatCollection(WorkloadManagementLogger.FormatWlmActivity(((INotificationManagerContext)this).ActivityScope, true));
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000B2FE File Offset: 0x000094FE
		void IAirSyncContext.SetDiagnosticValue(PropertyDefinition propDef, object value)
		{
			this.diagnosticsProperties[propDef] = value;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000B310 File Offset: 0x00009510
		void IAirSyncContext.ClearDiagnosticValue(PropertyDefinition propDef)
		{
			object obj;
			this.diagnosticsProperties.TryRemove(propDef, out obj);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000B32C File Offset: 0x0000952C
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			object[] array = new object[propertyDefinitionArray.Count];
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in propertyDefinitionArray)
			{
				array[num++] = this[propertyDefinition];
			}
			return array;
		}

		// Token: 0x170000A6 RID: 166
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				object result;
				try
				{
					object obj = null;
					if (this.diagnosticsProperties.TryGetValue(propertyDefinition, out obj))
					{
						result = obj;
					}
					else if (propertyDefinition == ConditionalHandlerSchema.SmtpAddress)
					{
						result = ((((IAirSyncContext)this).User == null) ? null : ((IAirSyncContext)this).User.SmtpAddress);
					}
					else if (propertyDefinition == ConditionalHandlerSchema.DisplayName)
					{
						result = ((((IAirSyncContext)this).User == null) ? null : ((IAirSyncContext)this).User.DisplayName);
					}
					else if (propertyDefinition == ConditionalHandlerSchema.TenantName)
					{
						result = ((((IAirSyncContext)this).User == null || ((IAirSyncContext)this).User.WindowsLiveId == null) ? null : SmtpAddress.Parse(((IAirSyncContext)this).User.WindowsLiveId).Domain);
					}
					else if (propertyDefinition == ConditionalHandlerSchema.WindowsLiveId)
					{
						result = ((IAirSyncContext)this).WindowsLiveId;
					}
					else if (propertyDefinition == ConditionalHandlerSchema.MailboxServer)
					{
						result = ((IAirSyncContext)this).User.ExchangePrincipal.MailboxInfo.Location.ServerFqdn;
					}
					else if (propertyDefinition == ConditionalHandlerSchema.MailboxDatabase)
					{
						result = ((((IAirSyncContext)this).User == null) ? null : ((IAirSyncContext)this).User.ExchangePrincipal.MailboxInfo.GetDatabaseGuid());
					}
					else if (propertyDefinition == ConditionalHandlerSchema.MailboxServerVersion)
					{
						result = ((((IAirSyncContext)this).User == null) ? null : ((IAirSyncContext)this).User.ExchangePrincipal.MailboxInfo.Location.ServerVersion);
					}
					else if (propertyDefinition == ConditionalHandlerSchema.IsMonitoringUser)
					{
						result = ((((IAirSyncContext)this).User == null) ? null : ((IAirSyncContext)this).User.IsMonitoringTestUser);
					}
					else if (propertyDefinition == ConditionalHandlerSchema.BudgetDelay)
					{
						double num;
						if (this.TryGetActivityContextStat(ActivityOperationType.UserDelay, out num))
						{
							result = num;
						}
						else
						{
							result = null;
						}
					}
					else if (propertyDefinition == ConditionalHandlerSchema.BudgetUsed)
					{
						double num2;
						if (this.TryGetActivityContextStat(ActivityOperationType.BudgetUsed, out num2))
						{
							result = num2;
						}
						else
						{
							result = null;
						}
					}
					else if (propertyDefinition == ConditionalHandlerSchema.BudgetLockedOut)
					{
						if (((IAirSyncContext)this).User != null)
						{
							ITokenBucket budgetTokenBucket = ((IAirSyncContext)this).User.GetBudgetTokenBucket();
							result = (budgetTokenBucket != null && budgetTokenBucket.Locked);
						}
						else
						{
							result = null;
						}
					}
					else if (propertyDefinition == ConditionalHandlerSchema.BudgetLockedUntil)
					{
						if (((IAirSyncContext)this).User != null)
						{
							ITokenBucket budgetTokenBucket2 = ((IAirSyncContext)this).User.GetBudgetTokenBucket();
							result = ((budgetTokenBucket2 != null) ? budgetTokenBucket2.LockedUntilUtc : new DateTime?(DateTime.MinValue));
						}
						else
						{
							result = null;
						}
					}
					else if (propertyDefinition == AirSyncConditionalHandlerSchema.DeviceId)
					{
						result = ((((INotificationManagerContext)this).DeviceIdentity != null) ? ((INotificationManagerContext)this).DeviceIdentity.DeviceId : null);
					}
					else if (propertyDefinition == AirSyncConditionalHandlerSchema.DeviceType)
					{
						result = ((((INotificationManagerContext)this).DeviceIdentity != null) ? ((INotificationManagerContext)this).DeviceIdentity.DeviceType : null);
					}
					else if (propertyDefinition == AirSyncConditionalHandlerSchema.ProtocolVersion)
					{
						result = ((IAirSyncContext)this).Request.Version;
					}
					else if (propertyDefinition == ConditionalHandlerSchema.ActivityId)
					{
						result = ((INotificationManagerContext)this).ActivityScope.ActivityId;
					}
					else if (propertyDefinition == ConditionalHandlerSchema.Cmd)
					{
						result = ((IAirSyncContext)this).Request.CommandType.ToString();
					}
					else if (propertyDefinition == AirSyncConditionalHandlerSchema.WbXmlRequestSize)
					{
						result = ((IAirSyncContext)this).Request.GetRawHttpRequest().TotalBytes;
					}
					else if (propertyDefinition == ConditionalHandlerSchema.ElapsedTime)
					{
						result = ExDateTime.UtcNow - ((INotificationManagerContext)this).RequestTime;
					}
					else if (propertyDefinition == AirSyncConditionalHandlerSchema.HttpStatus)
					{
						result = ((IAirSyncContext)this).Response.HttpStatusCode;
					}
					else if (propertyDefinition == AirSyncConditionalHandlerSchema.EasStatus)
					{
						int num3 = 0;
						if (this.protocolLogger.TryGetValue<int>(ProtocolLoggerData.StatusCode, out num3))
						{
							result = num3;
						}
						else
						{
							result = null;
						}
					}
					else if (propertyDefinition == AirSyncConditionalHandlerSchema.ProtocolError)
					{
						string text;
						if (this.protocolLogger.TryGetValue<string>(ProtocolLoggerData.Error, out text))
						{
							result = text;
						}
						else
						{
							result = null;
						}
					}
					else if (propertyDefinition == AirSyncConditionalHandlerSchema.Traces)
					{
						result = AirSyncInMemoryTraceHandler.GetInstance().GetExchangeDiagnosticsInfoData(default(DiagnosableParameters)).TraceData;
					}
					else if (propertyDefinition == ConditionalHandlerSchema.PostWlmElapsed)
					{
						TimeSpan timeSpan;
						if (((IAirSyncContext)this).TryGetElapsed(ConditionalHandlerIntermediateSchema.PostWlmStartTime, out timeSpan))
						{
							result = timeSpan;
						}
						else
						{
							result = null;
						}
					}
					else if (propertyDefinition == AirSyncConditionalHandlerSchema.ProxyElapsed)
					{
						TimeSpan timeSpan2;
						if (((IAirSyncContext)this).TryGetElapsed(ConditionalHandlerIntermediateSchema.ProxyStartTime, out timeSpan2))
						{
							result = timeSpan2;
						}
						else
						{
							result = null;
						}
					}
					else if (propertyDefinition == AirSyncConditionalHandlerSchema.ProxyFromServer)
					{
						result = (((IAirSyncContext)this).Request.WasProxied ? ((IAirSyncContext)this).Request.UserHostName : null);
					}
					else if (propertyDefinition == AirSyncConditionalHandlerSchema.WasProxied)
					{
						result = ((IAirSyncContext)this).Request.WasProxied;
					}
					else if (propertyDefinition == AirSyncConditionalHandlerSchema.TimeTracker)
					{
						result = ((IAirSyncContext)this).Tracker.ToString();
					}
					else
					{
						if (propertyDefinition == AirSyncConditionalHandlerSchema.XmlRequest)
						{
							try
							{
								XmlDocument xmlDocument = this.request.LoadRequestDocument();
								return (xmlDocument != null) ? AirSyncUtility.BuildOuterXml(xmlDocument) : null;
							}
							catch (Exception ex)
							{
								return ex.ToString();
							}
						}
						if (propertyDefinition == AirSyncConditionalHandlerSchema.XmlResponse)
						{
							result = ((((IAirSyncContext)this).Response != null && ((IAirSyncContext)this).Response.XmlDocument != null) ? AirSyncUtility.BuildOuterXml(((IAirSyncContext)this).Response.XmlDocument) : null);
						}
						else if (propertyDefinition == AirSyncConditionalHandlerSchema.ProtocolLoggerData)
						{
							string str = ((IAirSyncContext)this).ProtocolLogger.ToString();
							result = HttpUtility.UrlDecode(str);
						}
						else if (propertyDefinition == ConditionalHandlerSchema.ThrottlingPolicyName)
						{
							result = ((IAirSyncContext)this).GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.GetShortIdentityString());
						}
						else if (propertyDefinition == ConditionalHandlerSchema.MaxConcurrency)
						{
							result = ((IAirSyncContext)this).GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.EasMaxConcurrency);
						}
						else if (propertyDefinition == ConditionalHandlerSchema.MaxBurst)
						{
							result = ((IAirSyncContext)this).GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.EasMaxBurst);
						}
						else if (propertyDefinition == ConditionalHandlerSchema.CutoffBalance)
						{
							result = ((IAirSyncContext)this).GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.EasCutoffBalance);
						}
						else if (propertyDefinition == AirSyncConditionalHandlerSchema.EasMaxDevices)
						{
							result = ((IAirSyncContext)this).GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.EasMaxDevices);
						}
						else if (propertyDefinition == AirSyncConditionalHandlerSchema.EasMaxDeviceDeletesPerMonth)
						{
							result = ((IAirSyncContext)this).GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.EasMaxDeviceDeletesPerMonth);
						}
						else if (propertyDefinition == AirSyncConditionalHandlerSchema.EasMaxInactivityForDeviceCleanup)
						{
							result = ((IAirSyncContext)this).GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.EasMaxInactivityForDeviceCleanup);
						}
						else if (propertyDefinition == ConditionalHandlerSchema.ThrottlingPolicyScope)
						{
							result = ((IAirSyncContext)this).GetThrottlingPolicyValue((IThrottlingPolicy policy) => policy.ThrottlingPolicyScope);
						}
						else if (propertyDefinition == AirSyncConditionalHandlerSchema.RequestHeaders)
						{
							result = ((IAirSyncContext)this).Request.GetHeadersAsString();
						}
						else if (propertyDefinition == AirSyncConditionalHandlerSchema.ResponseHeaders)
						{
							result = ((IAirSyncContext)this).Response.GetHeadersAsString();
						}
						else if (propertyDefinition == AirSyncConditionalHandlerSchema.UserWLMData)
						{
							result = UserWorkloadManagerHandler.GetInstance().GetExchangeDiagnosticsInfoData(DiagnosableParameters.Create("dumpcache", false, true, string.Empty));
						}
						else if (propertyDefinition == AirSyncConditionalHandlerSchema.EmptyRequest)
						{
							result = ((IAirSyncContext)this).Request.IsEmpty;
						}
						else if (propertyDefinition == AirSyncConditionalHandlerSchema.PerCallTracing)
						{
							result = ((this.perCallTracer == null) ? null : this.perCallTracer.ToString());
						}
						else if (propertyDefinition == AirSyncConditionalHandlerSchema.IsConsumerOrganizationUser)
						{
							if (((IAirSyncContext)this).User != null)
							{
								result = ((IAirSyncContext)this).User.IsConsumerOrganizationUser;
							}
							else
							{
								result = null;
							}
						}
						else
						{
							result = null;
						}
					}
				}
				catch (AirSyncPermanentException arg)
				{
					AirSyncDiagnostics.TraceDebug<string, AirSyncPermanentException>(ExTraceGlobals.DiagnosticsTracer, this, "[AirSyncContext.this[]] Caught exception trying to retrieve diagnostics property '{0}'.  Ignoring.  Exception: {1}", propertyDefinition.Name, arg);
					result = null;
				}
				return result;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		object IAirSyncContext.GetThrottlingPolicyValue(Func<IThrottlingPolicy, object> func)
		{
			if (((IAirSyncContext)this).User == null || ((IAirSyncContext)this).User.Budget == null)
			{
				return null;
			}
			return func(((IAirSyncContext)this).User.Budget.ThrottlingPolicy);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000BC14 File Offset: 0x00009E14
		bool IAirSyncContext.TryGetElapsed(PropertyDefinition startTime, out TimeSpan elapsed)
		{
			elapsed = TimeSpan.Zero;
			object obj = this[startTime];
			if (obj != null)
			{
				ExDateTime dt = (ExDateTime)obj;
				elapsed = ExDateTime.UtcNow - dt;
				return true;
			}
			return false;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000BC54 File Offset: 0x00009E54
		private bool TryGetActivityContextStat(ActivityOperationType operationType, out double value)
		{
			value = 0.0;
			IEnumerable<KeyValuePair<OperationKey, OperationStatistics>> statistics = ((INotificationManagerContext)this).ActivityScope.Statistics;
			foreach (KeyValuePair<OperationKey, OperationStatistics> keyValuePair in statistics)
			{
				if (keyValuePair.Key.ActivityOperationType == operationType)
				{
					TotalOperationStatistics totalOperationStatistics = keyValuePair.Value as TotalOperationStatistics;
					if (totalOperationStatistics != null)
					{
						value = totalOperationStatistics.Total;
						return true;
					}
					AverageOperationStatistics averageOperationStatistics = keyValuePair.Value as AverageOperationStatistics;
					if (averageOperationStatistics != null)
					{
						value = (double)averageOperationStatistics.CumulativeAverage;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000BD00 File Offset: 0x00009F00
		BudgetKey INotificationManagerContext.BudgetKey
		{
			get
			{
				return ((IAirSyncContext)this).User.BudgetKey;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000BD0D File Offset: 0x00009F0D
		string INotificationManagerContext.SmtpAddress
		{
			get
			{
				return ((IAirSyncContext)this).User.SmtpAddress;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000BD1A File Offset: 0x00009F1A
		DeviceIdentity INotificationManagerContext.DeviceIdentity
		{
			get
			{
				return ((IAirSyncContext)this).Request.DeviceIdentity;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000BD27 File Offset: 0x00009F27
		Guid INotificationManagerContext.MdbGuid
		{
			get
			{
				return ((IAirSyncContext)this).User.ExchangePrincipal.MailboxInfo.GetDatabaseGuid();
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000BD40 File Offset: 0x00009F40
		int INotificationManagerContext.MailboxPolicyHash
		{
			get
			{
				PolicyData policyData = ADNotificationManager.GetPolicyData(((IAirSyncContext)this).User);
				if (policyData != null)
				{
					return policyData.GetHashCode(((IAirSyncContext)this).Request.Version);
				}
				return 0;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000BD70 File Offset: 0x00009F70
		uint INotificationManagerContext.PolicyKey
		{
			get
			{
				if (((IAirSyncContext)this).Request.PolicyKey != null)
				{
					return ((IAirSyncContext)this).Request.PolicyKey.Value;
				}
				return 0U;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000BDA7 File Offset: 0x00009FA7
		int INotificationManagerContext.AirSyncVersion
		{
			get
			{
				return ((IAirSyncContext)this).Request.Version;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000BDB4 File Offset: 0x00009FB4
		ExDateTime INotificationManagerContext.RequestTime
		{
			get
			{
				return new ExDateTime(ExTimeZone.UtcTimeZone, this.httpContext.Timestamp.ToUniversalTime());
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000BDDE File Offset: 0x00009FDE
		Guid INotificationManagerContext.MailboxGuid
		{
			get
			{
				return ((IAirSyncContext)this).User.MailboxGuid;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000BDEB File Offset: 0x00009FEB
		CommandType INotificationManagerContext.CommandType
		{
			get
			{
				return ((IAirSyncContext)this).Request.CommandType;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000BDF8 File Offset: 0x00009FF8
		// (set) Token: 0x06000178 RID: 376 RVA: 0x0000BE00 File Offset: 0x0000A000
		IActivityScope INotificationManagerContext.ActivityScope { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000BE09 File Offset: 0x0000A009
		IAccountValidationContext INotificationManagerContext.AccountValidationContext
		{
			get
			{
				if (this.httpContext.Items.Contains("AccountValidationContext"))
				{
					return (IAccountValidationContext)this.httpContext.Items["AccountValidationContext"];
				}
				return null;
			}
		}

		// Token: 0x040001D0 RID: 464
		private static readonly Dictionary<EasFeature, bool> EmptyOverrides = new Dictionary<EasFeature, bool>();

		// Token: 0x040001D1 RID: 465
		private Dictionary<EasFeature, bool> flightingOverrides;

		// Token: 0x040001D2 RID: 466
		private ConcurrentDictionary<string, Participant> cachedParticipants = new ConcurrentDictionary<string, Participant>();

		// Token: 0x040001D3 RID: 467
		private HttpContext httpContext;

		// Token: 0x040001D4 RID: 468
		private IAirSyncRequest request;

		// Token: 0x040001D5 RID: 469
		private IAirSyncResponse response;

		// Token: 0x040001D6 RID: 470
		private StringBuilder perCallTracer;

		// Token: 0x040001D7 RID: 471
		private IAirSyncUser user;

		// Token: 0x040001D8 RID: 472
		private ProtocolLogger protocolLogger = new ProtocolLogger();

		// Token: 0x040001D9 RID: 473
		private int participantCacheHits;

		// Token: 0x040001DA RID: 474
		private int participantCacheMisses;

		// Token: 0x040001DB RID: 475
		private ConcurrentDictionary<PropertyDefinition, object> diagnosticsProperties = new ConcurrentDictionary<PropertyDefinition, object>();

		// Token: 0x02000017 RID: 23
		private enum EasTraceType
		{
			// Token: 0x040001E8 RID: 488
			None,
			// Token: 0x040001E9 RID: 489
			DbgT,
			// Token: 0x040001EA RID: 490
			Dbg = 1,
			// Token: 0x040001EB RID: 491
			Wrn,
			// Token: 0x040001EC RID: 492
			WrnT = 2,
			// Token: 0x040001ED RID: 493
			Err,
			// Token: 0x040001EE RID: 494
			ErrT = 3,
			// Token: 0x040001EF RID: 495
			FtlT,
			// Token: 0x040001F0 RID: 496
			Ftl = 4,
			// Token: 0x040001F1 RID: 497
			Inf,
			// Token: 0x040001F2 RID: 498
			InfT = 5,
			// Token: 0x040001F3 RID: 499
			Prf,
			// Token: 0x040001F4 RID: 500
			PrfT = 6,
			// Token: 0x040001F5 RID: 501
			Fnc,
			// Token: 0x040001F6 RID: 502
			FncT = 7,
			// Token: 0x040001F7 RID: 503
			PfdT,
			// Token: 0x040001F8 RID: 504
			Pfd = 8,
			// Token: 0x040001F9 RID: 505
			Flt,
			// Token: 0x040001FA RID: 506
			FltI = 9
		}
	}
}
