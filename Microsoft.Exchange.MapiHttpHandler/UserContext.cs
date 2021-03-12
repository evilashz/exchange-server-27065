using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000051 RID: 81
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UserContext
	{
		// Token: 0x060002DC RID: 732 RVA: 0x000122B4 File Offset: 0x000104B4
		public UserContext(string userName, string userPrincipalName, string userSecurityIdentifier, string userAuthIdentifier, string organization)
		{
			this.userName = userName;
			this.userPrincipalName = userPrincipalName;
			this.userSecurityIdentifier = userSecurityIdentifier;
			this.userAuthIdentifier = userAuthIdentifier;
			this.organization = organization;
			this.creationDateTime = ExDateTime.UtcNow;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00012318 File Offset: 0x00010518
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00012320 File Offset: 0x00010520
		public string UserPrincipalName
		{
			get
			{
				return this.userPrincipalName;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00012328 File Offset: 0x00010528
		public string UserSecurityIdentifier
		{
			get
			{
				return this.userSecurityIdentifier;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00012330 File Offset: 0x00010530
		public string UserAuthIdentifier
		{
			get
			{
				return this.userAuthIdentifier;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00012338 File Offset: 0x00010538
		public string Organization
		{
			get
			{
				return this.organization;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00012340 File Offset: 0x00010540
		public bool IsActive
		{
			get
			{
				bool result;
				lock (this.userContextLock)
				{
					TimeSpan t = TimeSpan.FromMilliseconds((double)(Environment.TickCount - this.lastActivity));
					result = (this.sessionContexts.Count > 0 || this.activityCount > 0 || t < Constants.UserContextIdleTimeout);
				}
				return result;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x000123B4 File Offset: 0x000105B4
		public ExDateTime CreationDateTime
		{
			get
			{
				return this.creationDateTime;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000123BC File Offset: 0x000105BC
		public SessionContextActivity CreateSessionContextActivity(string mailboxIdentifier, SessionContextIdentifier sessionContextIdentifier, TimeSpan idleTimeout)
		{
			SessionContext sessionContext = new SessionContext(this, mailboxIdentifier, sessionContextIdentifier, idleTimeout, null);
			SessionContextActivity result;
			lock (this.userContextLock)
			{
				if (this.sessionContexts.ContainsKey(sessionContext.Id))
				{
					throw new InvalidOperationException("Context identifier already exists");
				}
				this.sessionContexts[sessionContext.Id] = sessionContext;
				SessionContextActivity sessionContextActivity;
				if (!SessionContextActivity.TryCreate(sessionContext, out sessionContextActivity))
				{
					throw ProtocolException.FromResponseCode((LID)57600, "Unable to create session context activity object.", ResponseCode.ContextNotFound, null);
				}
				result = sessionContextActivity;
			}
			return result;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00012454 File Offset: 0x00010654
		public bool TryGetSessionContextActivity(long id, TimeSpan idleTimeout, out SessionContextActivity sessionContextActivity, out Exception failureException)
		{
			sessionContextActivity = null;
			failureException = null;
			bool result;
			lock (this.userContextLock)
			{
				SessionContext sessionContext;
				if (!this.sessionContexts.TryGetValue(id, out sessionContext))
				{
					failureException = ProtocolException.FromResponseCode((LID)51744, "Unable to find session context based on cookie.", ResponseCode.ContextNotFound, null);
					result = false;
				}
				else if (!SessionContextActivity.TryCreate(sessionContext, out sessionContextActivity))
				{
					failureException = ProtocolException.FromResponseCode((LID)45600, "Unable to create session context activity object.", ResponseCode.ContextNotFound, null);
					result = false;
				}
				else
				{
					sessionContext.IdleTimeout = idleTimeout;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000124FC File Offset: 0x000106FC
		public bool TryGetSessionContextInfo(out SessionContextInfo[] sessionContextInfoArray)
		{
			sessionContextInfoArray = null;
			bool result;
			lock (this.userContextLock)
			{
				if (this.sessionContexts.Count > 0)
				{
					sessionContextInfoArray = (from x in this.sessionContexts
					select x.Value.GetSessionContextInfo()).ToArray<SessionContextInfo>();
				}
				result = (sessionContextInfoArray != null);
			}
			return result;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00012580 File Offset: 0x00010780
		public void AddReference()
		{
			lock (this.userContextLock)
			{
				this.lastActivity = Environment.TickCount;
				this.activityCount++;
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000125D4 File Offset: 0x000107D4
		public void ReleaseReference()
		{
			lock (this.userContextLock)
			{
				this.lastActivity = Environment.TickCount;
				this.activityCount--;
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00012628 File Offset: 0x00010828
		public void GatherExpiredSessionContexts(List<SessionContext> expiredSessionContextList, out ExDateTime nextExpiration)
		{
			nextExpiration = ExDateTime.MaxValue;
			lock (this.userContextLock)
			{
				if (this.sessionContexts.Count > 0)
				{
					List<long> list = new List<long>();
					foreach (KeyValuePair<long, SessionContext> keyValuePair in this.sessionContexts)
					{
						if (keyValuePair.Value != null)
						{
							if (keyValuePair.Value.IsRundown)
							{
								list.Add(keyValuePair.Key);
								expiredSessionContextList.Add(keyValuePair.Value);
							}
							else
							{
								ExDateTime expires = keyValuePair.Value.Expires;
								if (expires < nextExpiration)
								{
									nextExpiration = expires;
								}
							}
						}
						else
						{
							list.Add(keyValuePair.Key);
						}
					}
					foreach (long key in list)
					{
						this.sessionContexts.Remove(key);
					}
				}
			}
		}

		// Token: 0x0400015C RID: 348
		private readonly object userContextLock = new object();

		// Token: 0x0400015D RID: 349
		private readonly Dictionary<long, SessionContext> sessionContexts = new Dictionary<long, SessionContext>();

		// Token: 0x0400015E RID: 350
		private readonly string userName;

		// Token: 0x0400015F RID: 351
		private readonly string userPrincipalName;

		// Token: 0x04000160 RID: 352
		private readonly string userSecurityIdentifier;

		// Token: 0x04000161 RID: 353
		private readonly string userAuthIdentifier;

		// Token: 0x04000162 RID: 354
		private readonly string organization;

		// Token: 0x04000163 RID: 355
		private readonly ExDateTime creationDateTime;

		// Token: 0x04000164 RID: 356
		private int activityCount;

		// Token: 0x04000165 RID: 357
		private int lastActivity = Environment.TickCount;
	}
}
