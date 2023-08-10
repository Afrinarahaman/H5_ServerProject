using H5_ServerProject.Data;
using H5_ServerProject.Database;
using H5_ServerProject.Entity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace H5_ServerProject.Services
{
    public interface IToDoService
    {

        public Task<List<ToDoItem>> LoadToDoList(Guid userid);

        public Task CreateToDoItem(ToDoItem item);
        public Task EditToDoItem(ToDoItem item);
        public Task DeleteToDoItem(ToDoItem item);

    }
    public class ToDoItemService : IToDoService
    {
        private readonly toDoDatabaseContext _context;
        private readonly IDataProtector _dataprotector;

        
        

        public ToDoItemService(toDoDatabaseContext context, IDataProtectionProvider dataProtectionProvider)
        {
            _context = context;
            _dataprotector= dataProtectionProvider.CreateProtector("PURPOSE");

        }

        private List<ToDoItem> ToDoItems = new List<ToDoItem>();

        public async Task<List<ToDoItem>> LoadToDoList(Guid userid)
        {
            ToDoItems= await _context.ToDos.Where(i => i.User_Id== userid).AsNoTracking().ToListAsync();
            List<ToDoItem> unprotected = new();
            foreach (var item in ToDoItems)
            {
                try

                {
                    item.Title=_dataprotector.Unprotect(item.Title);
                    item.Description=_dataprotector.Unprotect(item.Description);
                    unprotected.Add(item);
                }
                catch (Exception)
                {

                    throw;
                }
            }


            return unprotected;
        }
        public async Task CreateToDoItem(ToDoItem item)
        {
            item.Title=_dataprotector.Protect(item.Title);
            item.Description=_dataprotector.Protect(item.Description);
            //item.User_Id= await _guidservice.GetGuidID();

            _context.ToDos.Add(item);
            // _context.ToDos.Entry(item).State = EntityState.Added;
            await _context.SaveChangesAsync();
            //return item;

        }


       
        public async Task EditToDoItem(ToDoItem item)
        {
            //ToDoItem updateToDo = await _context.ToDos.FirstOrDefaultAsync(a => a.Id== item.Id);
            if (item!=null)
            {

                item.Title=_dataprotector.Protect(item.Title);
                item.Description=_dataprotector.Protect(item.Description);
                _context.ToDos.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                    
                                  
            }
            //return item;
        }
        public async Task DeleteToDoItem(ToDoItem item)
        {
            ToDoItem deleteToDo = await _context.ToDos.FirstOrDefaultAsync(a => a.Id== item.Id);
            if (deleteToDo!=null)
            {
                _context.ToDos.Remove(deleteToDo);
                await _context.SaveChangesAsync();
            }
           
        }
    }
}
