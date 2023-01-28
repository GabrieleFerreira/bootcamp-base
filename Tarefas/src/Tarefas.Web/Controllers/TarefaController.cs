using Microsoft.AspNetCore.Mvc;
using Tarefas.Web.Models;
using Tarefas.DTO;
using Tarefas.DAO;
using AutoMapper;
namespace Tarefas.Web.Controllers
{
    public class TarefaController : Controller
    {
        public List<TarefaViewModel> listaDeTarefas { get; set; }

        private readonly IMapper _mapper;
        private readonly ITarefaDAO _tarefaDAO;


        public TarefaController(ITarefaDAO tarefaDAO, IMapper mapper1)
        {            
          _tarefaDAO = tarefaDAO;
        }
        
        public IActionResult Details(int id)
        {      
            var tarefaDTO = _tarefaDAO.Consultar(id);

            var tarefa = new TarefaViewModel()
            {
                Id = tarefaDTO.Id,
                Titulo = tarefaDTO.Titulo,
                Descricao = tarefaDTO.Descricao,
                Concluida =tarefaDTO.Concluida
            };
            return View(tarefa);
        }

        public IActionResult Index()
        {
            var listaDeTarefasDTO = _tarefaDAO.Consultar();

            listaDeTarefas = new List<TarefaViewModel>();

            foreach (var tarefaDTO in listaDeTarefasDTO)
            {
                listaDeTarefas.Add(new TarefaViewModel()
                {
                    Id = tarefaDTO.Id,
                    Titulo = tarefaDTO.Titulo,
                    Descricao = tarefaDTO.Descricao,
                    Concluida =tarefaDTO.Concluida
                });
            }
            return View(listaDeTarefas);
        }

        public IActionResult Create()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]  
        public IActionResult Create(TarefaViewModel tarefa)
        {
            var tarefaDTO = new TarefaDTO 
            {
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Concluida = tarefa.Concluida
            };

            
            _tarefaDAO.Criar(tarefaDTO);

            return View();
        }

        public IActionResult Update(TarefaViewModel tarefa){
            var tarefaDTO = new TarefaDTO {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Concluida = tarefa.Concluida
            };
            
            _tarefaDAO.Atualizar(tarefaDTO);

            return RedirectToAction("Index");

        }

        public IActionResult Update(int id) 
        {
            
            var tarefaDTO = _tarefaDAO.Consultar(id);
  
            var tarefa = new TarefaViewModel(){
                Id = tarefaDTO.Id,
                    Titulo = tarefaDTO.Titulo,
                    Descricao = tarefaDTO.Descricao,
                    Concluida =tarefaDTO.Concluida

            };

            return View(tarefa);
        }

        public IActionResult Delete (int id){
           
            _tarefaDAO.Excluir(id);

            return RedirectToAction("Index");
        }
        
    }
}