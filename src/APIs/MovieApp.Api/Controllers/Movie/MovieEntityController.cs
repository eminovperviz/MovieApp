namespace MovieApp.Web.Controllers;

public class MovieEntityController : BaseController
{
    private readonly IMovieEntityService _movieEntityService;

    public MovieEntityController(IMovieEntityService movieEntityService)
    {
        _movieEntityService = movieEntityService;
    }

    /// <summary>
    /// Add new MovieEntity
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("Add")]
    [ProducesResponseType(typeof(MovieEntityDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(MovieEntityAddRequest model)
    {
        return Ok(await _movieEntityService.AddAsync(model));
    }


    /// <summary>
    /// Update MovieEntity
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("Update")]
    public async Task<IActionResult> Update(MovieEntityUpdateRequest model)
    {
        await _movieEntityService.EditAsync(model);

        return Ok();
    }


    /// <summary>
    /// Get MovieEntity data by ID (This endpoint generally used for update MovieEntity)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("GetForUpdateById")]
    [ProducesResponseType(typeof(MovieEntityUpdateRequest), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForUpdateById(int id)
    {
        return Ok(await _movieEntityService.GetForUpdateByIdAsync(id));
    }


    /// <summary>
    /// Get MovieEntity data by ID (This endpoint generally used for view of MovieEntity data. It does not contains relations)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("GetById")]
    [ProducesResponseType(typeof(MovieEntityDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _movieEntityService.GetByIdAsync(id));
    }

    /// <summary>
    /// Delete MovieEntity
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await _movieEntityService.DeleteByIdAsync(id);

        return Ok();
    }


    /// <summary>
    ///  Get  MovieEntity data. (This method use for server side pagination)
    /// </summary>
    /// <param name="pagingRequest"></param>
    /// <returns></returns>
    [HttpPost("GetTableAsync")]
    [ProducesResponseType(typeof(IEnumerable<MovieEntityTableListResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTableAsync([FromBody] PagingRequest pagingRequest)
    {
        // filter example 
        //pagingRequest = new PagingRequest
        //{
        //    Filters = new List<PagingRequestFilter>()
        //    {
        //        new PagingRequestFilter
        //        {
        //            EqualityType = "Equal",
        //            FieldName= "ReleaseYear",
        //            Value = "2012"
        //        }
        //    }
        //};

        return Ok(await _movieEntityService.GetTableAsync(pagingRequest));
    }



    /// <summary>
    /// Get all MovieEntity data by title
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    [HttpPost("GetTableByTitleAsync")]
    [ProducesResponseType(typeof(IEnumerable<MovieEntityTableListResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTableByTitleAsync([FromBody] string title)
    {
        return Ok(await _movieEntityService.GetTableByTitleAsync(title));
    }



    /// <summary>
    /// Get all MovieEntity data by releaseYear
    /// </summary>
    /// <param name="releaseYear"></param>
    /// <returns></returns>
    [HttpPost("GetTableByReleaseYearAsync")]
    [ProducesResponseType(typeof(IEnumerable<MovieEntityTableListResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTableByReleaseYearAsync([FromBody] int releaseYear)
    {
        return Ok(await _movieEntityService.GetTableByReleaseYearAsync(releaseYear));
    }



}
