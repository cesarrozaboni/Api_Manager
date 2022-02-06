using Manager.Api.Controllers.ViewModel;

namespace Manager.Api.Utilities
{
    public static class Responses
    {
        public static ResultViewModel ApplicationErrorMessage()
        {
            return new ResultViewModel {
                                          Message = "Ocorreu um erro interno na aplicação, por favor tente novamente!",
                                          Success = false,
                                          Data = null
                                       };
        }

        public static ResultViewModel DomainErrorMessage(string message)
        {
            return new ResultViewModel
            {
                Message = message,
                Success = false,
                Data = null
            };
        }

        public static ResultViewModel DomainErrorMessage(string message, IReadOnlyCollection<string> errors)
        {
            return new ResultViewModel
            {
                Message = message,
                Success = false,
                Data = errors
            };
        }

        public static ResultViewModel UnauthorizedErrorMessage()
        {
            return new ResultViewModel
            {
                Message = "A combinação de Login e senha esta incorreta",
                Success = false,
                Data = null
            };
        }
    }
}
