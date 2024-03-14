using GZIJ20241303.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GZIJ20241303.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly GZIJ20241303DbContext _context;

        public ProveedoresController(GZIJ20241303DbContext context)
        {
            _context = context;
        }
        // GET: Proveedores
        public async Task<IActionResult> Index()
        {
            return _context.Proveedores != null ?
                        View(await _context.Proveedores.ToListAsync()) :
                        Problem("Entity set 'GZIJ20241303DbContext.Proveedores'  is null.");
        }

        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores
                    .Include(s => s.DireccionesProveedors)
                .FirstOrDefaultAsync(m => m.IdProveedor == id);
            if (proveedore == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Details";
            return View(proveedore);
        }
        // GET: Proveedores/Create
        public IActionResult Create()
        {
            var prov = new Proveedor();
            prov.FechaRegistro = DateTime.Now;

            prov.DireccionesProveedors = new List<DireccionesProveedor>();
            prov.DireccionesProveedors.Add(new DireccionesProveedor
            {

            });
            ViewBag.Accion = "Create";
            return View(prov);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProveedor,Nombre,Telefono,CorreoElectronico,Producto,FechaRegistro,DireccionesProveedors")] Proveedor proveedore)
        {

            _context.Add(proveedore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ;
        }

        public ActionResult AgregarDetalles([Bind("IdProveedor,Nombre,Telefono,CorreoElectronico,Producto,FechaRegistro,DireccionesProveedors")] Proveedor proveedor, string accion)
        {
            proveedor.DireccionesProveedors.Add(new DireccionesProveedor { });
            ViewBag.Accion = accion;
            return View(accion, proveedor);
        }

        public ActionResult EliminarDetalles([Bind("IdProveedor,Nombre,Telefono,CorreoElectronico,Producto,FechaRegistro,DireccionesProveedors")] Proveedor proveedor,
           int index, string accion)
        {
            var det = proveedor.DireccionesProveedors[index];
            if (accion == "Edit" && det.IdDireccion > 0)
            {
                det.IdDireccion = det.IdDireccion * -1;
            }
            else
            {
                proveedor.DireccionesProveedors.RemoveAt(index);
            }
            ViewBag.Accion = accion;
            return View(accion, proveedor);
        }


        // GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores
            .Include(s => s.DireccionesProveedors)
            .FirstAsync(s => s.IdProveedor == id); ;
            if (proveedore == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Edit";
            return View(proveedore);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdProveedor,Nombre,Telefono,CorreoElectronico,Producto,FechaRegistro,DireccionesProveedors")] Proveedor proveedore)
        {

            try
            {
                // Obtener los datos de la base de datos que van a ser modificados
                var proveedorUpdate = await _context.Proveedores
                        .Include(s => s.DireccionesProveedors)
                        .FirstAsync(s => s.IdProveedor == proveedore.IdProveedor);
                proveedorUpdate.Nombre = proveedore.Nombre;
                proveedorUpdate.Producto = proveedore.Producto;
                proveedorUpdate.FechaRegistro = proveedore.FechaRegistro;
                proveedorUpdate.Telefono = proveedore.Telefono;
                proveedorUpdate.CorreoElectronico = proveedore.CorreoElectronico;
                // Obtener todos los detalles que seran nuevos y agregarlos a la base de datos
                var detNew = proveedore.DireccionesProveedors.Where(s => s.IdDireccion == 0);
                foreach (var d in detNew)
                {
                    proveedorUpdate.DireccionesProveedors.Add(d);
                }
                // Obtener todos los detalles que seran modificados y actualizar a la base de datos
                var detUpdate = proveedore.DireccionesProveedors.Where(s => s.IdDireccion > 0);
                foreach (var d in detUpdate)
                {
                    var det = proveedorUpdate.DireccionesProveedors.FirstOrDefault(s => s.IdDireccion == d.IdDireccion);
                    det.Direccion = d.Direccion;

                    det.Ciudad = d.Ciudad;
                    det.Pais = d.Pais;
                }
                // Obtener todos los detalles que seran eliminados y actualizar a la base de datos
                var delDet = proveedore.DireccionesProveedors.Where(s => s.IdDireccion < 0).ToList();
                if (delDet != null && delDet.Count > 0)
                {
                    foreach (var d in delDet)
                    {
                        d.IdDireccion = d.IdDireccion * -1;
                        var det = proveedorUpdate.DireccionesProveedors.FirstOrDefault(s => s.IdDireccion == d.IdDireccion);
                        _context.Remove(det);

                    }
                }
                // Aplicar esos cambios a la base de datos
                _context.Update(proveedorUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(proveedore.IdProveedor))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }


        // GET: Proveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores
                .Include(s => s.DireccionesProveedors)
                .FirstOrDefaultAsync(m => m.IdProveedor == id);
            if (proveedor == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Delete";
            return View(proveedor);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Proveedores == null)
            {
                return Problem("Entity set 'GZIJ20241303DbContext.Proveedores'  is null.");
            }
            var proveedore = await _context.Proveedores
                .FindAsync(id);
            if (proveedore != null)
            {
                _context.Proveedores.Remove(proveedore);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedorExists(int id)
        {
            return (_context.Proveedores?.Any(e => e.IdProveedor == id)).GetValueOrDefault();
        }
    }

}
