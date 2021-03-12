using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.UM.PersonalAutoAttendant;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004CA RID: 1226
	[OwaEventNamespace("PaaOptions")]
	internal sealed class PersonalAutoAttendantOptionsEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002ED8 RID: 11992 RVA: 0x0010D6BB File Offset: 0x0010B8BB
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(PersonalAutoAttendantOptionsEventHandler));
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x0010D6CC File Offset: 0x0010B8CC
		[OwaEvent("Move")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("Id", typeof(string), false, false)]
		[OwaEventParameter("Ids", typeof(string), true, true)]
		[OwaEventParameter("op", typeof(int), false, false)]
		public void Move()
		{
			using (IPAAStore ipaastore = PAAStore.Create(base.UserContext.ExchangePrincipal))
			{
				IList<PersonalAutoAttendant> list = null;
				ipaastore.TryGetAutoAttendants(PAAValidationMode.StopOnFirstError, out list);
				Guid identity = new Guid(Convert.FromBase64String((string)base.GetParameter("Id")));
				int num;
				PersonalAutoAttendant personalAutoAttendant = PersonalAutoAttendantOptionsEventHandler.FindAutoAttendantByGuid(list, identity, out num);
				if (personalAutoAttendant != null)
				{
					if (this.IsOrderChanged(list))
					{
						base.RenderPartialFailure(-846213614, OwaEventHandlerErrorCode.UnexpectedError);
					}
					else
					{
						int num2 = (int)base.GetParameter("op");
						if ((num2 != 1 || num == 0) && (num2 != 2 || num >= list.Count - 1))
						{
							throw new OwaInvalidRequestException("Event name and parameter doesn't match");
						}
						list.RemoveAt(num);
						if (num2 == 1)
						{
							list.Insert(num - 1, personalAutoAttendant);
						}
						else
						{
							list.Insert(num + 1, personalAutoAttendant);
						}
						ipaastore.Save(list);
						ipaastore.TryGetAutoAttendants(PAAValidationMode.StopOnFirstError, out list);
					}
				}
				else
				{
					base.RenderPartialFailure(-289549140, OwaEventHandlerErrorCode.ItemNotFound);
				}
				this.RefreshList(list);
			}
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x0010D7D8 File Offset: 0x0010B9D8
		[OwaEvent("Enable")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("Id", typeof(string), false, false)]
		[OwaEventParameter("Ids", typeof(string), true, true)]
		[OwaEventParameter("op", typeof(int), false, false)]
		public void Enable()
		{
			using (IPAAStore ipaastore = PAAStore.Create(base.UserContext.ExchangePrincipal))
			{
				IList<PersonalAutoAttendant> list = null;
				ipaastore.TryGetAutoAttendants(PAAValidationMode.StopOnFirstError, out list);
				Guid identity = new Guid(Convert.FromBase64String((string)base.GetParameter("Id")));
				int index;
				PersonalAutoAttendant personalAutoAttendant = PersonalAutoAttendantOptionsEventHandler.FindAutoAttendantByGuid(list, identity, out index);
				if (personalAutoAttendant != null)
				{
					int num = (int)base.GetParameter("op");
					if (num == 3)
					{
						personalAutoAttendant.Enabled = true;
					}
					else
					{
						if (num != 4)
						{
							throw new OwaInvalidRequestException("Event name and parameter doesn't match");
						}
						personalAutoAttendant.Enabled = false;
					}
					ipaastore.Save(list);
					ipaastore.TryGetAutoAttendants(PAAValidationMode.StopOnFirstError, out list);
					personalAutoAttendant = list[index];
					this.Writer.Write("<div id=\"ret\" enbl=");
					this.Writer.Write(personalAutoAttendant.Enabled ? 1 : 0);
					this.Writer.Write("></div>");
				}
				else
				{
					base.RenderPartialFailure(-289549140, OwaEventHandlerErrorCode.ItemNotFound);
					this.RefreshList(list);
				}
			}
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x0010D8F0 File Offset: 0x0010BAF0
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEvent("Delete")]
		[OwaEventParameter("Id", typeof(string), false, false)]
		[OwaEventParameter("Ids", typeof(string), true, true)]
		public void Delete()
		{
			using (IPAAStore ipaastore = PAAStore.Create(base.UserContext.ExchangePrincipal))
			{
				Guid identity = new Guid(Convert.FromBase64String((string)base.GetParameter("Id")));
				PersonalAutoAttendant personalAutoAttendant = null;
				ipaastore.TryGetAutoAttendant(identity, PAAValidationMode.None, out personalAutoAttendant);
				if (personalAutoAttendant != null)
				{
					ipaastore.DeleteAutoAttendant(identity);
				}
				IList<PersonalAutoAttendant> personalAutoAttendants = null;
				ipaastore.TryGetAutoAttendants(PAAValidationMode.StopOnFirstError, out personalAutoAttendants);
				this.RefreshList(personalAutoAttendants);
			}
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x0010D970 File Offset: 0x0010BB70
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEvent("Refresh")]
		public void Refresh()
		{
			using (IPAAStore ipaastore = PAAStore.Create(base.UserContext.ExchangePrincipal))
			{
				IList<PersonalAutoAttendant> personalAutoAttendants = null;
				ipaastore.TryGetAutoAttendants(PAAValidationMode.StopOnFirstError, out personalAutoAttendants);
				this.RefreshList(personalAutoAttendants);
			}
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x0010D9C0 File Offset: 0x0010BBC0
		private static PersonalAutoAttendant FindAutoAttendantByGuid(IList<PersonalAutoAttendant> autoattendants, Guid identity, out int index)
		{
			index = -1;
			PersonalAutoAttendant result = null;
			if (autoattendants != null)
			{
				for (int i = 0; i < autoattendants.Count; i++)
				{
					if (autoattendants[i].Identity == identity)
					{
						index = i;
						result = autoattendants[i];
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x0010DA08 File Offset: 0x0010BC08
		private void RefreshList(IList<PersonalAutoAttendant> personalAutoAttendants)
		{
			PersonalAutoAttendantListView personalAutoAttendantListView = new PersonalAutoAttendantListView(base.UserContext, personalAutoAttendants);
			personalAutoAttendantListView.Render(this.Writer);
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x0010DA30 File Offset: 0x0010BC30
		private bool IsOrderChanged(IList<PersonalAutoAttendant> personalAutoAttendants)
		{
			if (base.IsParameterSet("Ids"))
			{
				string[] array = (string[])base.GetParameter("Ids");
				if (array.Length != personalAutoAttendants.Count)
				{
					return true;
				}
				for (int i = 0; i < personalAutoAttendants.Count; i++)
				{
					if (!personalAutoAttendants[i].Identity.Equals(new Guid(Convert.FromBase64String(array[i]))))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040020B2 RID: 8370
		public const string EventNamespace = "PaaOptions";

		// Token: 0x040020B3 RID: 8371
		public const string MethodMove = "Move";

		// Token: 0x040020B4 RID: 8372
		public const string MethodEnable = "Enable";

		// Token: 0x040020B5 RID: 8373
		public const string MethodDelete = "Delete";

		// Token: 0x040020B6 RID: 8374
		public const string MethodRefresh = "Refresh";

		// Token: 0x040020B7 RID: 8375
		public const string Id = "Id";

		// Token: 0x040020B8 RID: 8376
		public const string Ids = "Ids";

		// Token: 0x040020B9 RID: 8377
		public const string Operation = "op";

		// Token: 0x040020BA RID: 8378
		public const int OperationMoveUp = 1;

		// Token: 0x040020BB RID: 8379
		public const int OperationMoveDown = 2;

		// Token: 0x040020BC RID: 8380
		public const int OperationEnable = 3;

		// Token: 0x040020BD RID: 8381
		public const int OperationDisable = 4;
	}
}
