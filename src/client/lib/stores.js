import { writable, derived } from 'svelte/store';

// Mock data for development
const mockEvents = [
	{
		id: 1,
		title: 'Sommarfestival i parken',
		description: 'En fantastisk musikfestival med lokala artister',
		date: '2024-07-15',
		time: '18:00',
		location: 'Stadsparken',
		category: 'musik',
		image: 'https://via.placeholder.com/400x200',
		price: 250,
		organizer: 'Stadsteater'
	},
	{
		id: 2,
		title: 'Konstutställning - Moderna Mästare',
		description: 'Utställning med samtida konst från lokala konstnärer',
		date: '2024-07-20',
		time: '10:00',
		location: 'Konstmuseet',
		category: 'konst',
		image: 'https://via.placeholder.com/400x200',
		price: 120,
		organizer: 'Konstmuseet'
	},
	{
		id: 3,
		title: 'Matmarknad',
		description: 'Lokala producenter visar upp sina bästa varor',
		date: '2024-07-22',
		time: '09:00',
		location: 'Stora Torget',
		category: 'mat',
		image: 'https://via.placeholder.com/400x200',
		price: 0,
		organizer: 'Kommun'
	}
];

const mockCategories = [
	{ id: 'musik', name: 'Musik', icon: '🎵' },
	{ id: 'konst', name: 'Konst', icon: '🎨' },
	{ id: 'mat', name: 'Mat & Dryck', icon: '🍽️' },
	{ id: 'sport', name: 'Sport', icon: '⚽' },
	{ id: 'teater', name: 'Teater', icon: '🎭' },
	{ id: 'barn', name: 'Barnaktiviteter', icon: '👶' }
];

// Events store
export const events = writable(mockEvents);

// Categories store
export const categories = writable(mockCategories);

// Filter store
export const filters = writable({
	category: '',
	date: '',
	priceRange: 'all', // 'free', 'paid', 'all'
	searchTerm: ''
});

// Derived store for filtered events
export const filteredEvents = derived(
	[events, filters],
	([$events, $filters]) => {
		let filtered = [...$events];

		// Filter by category
		if ($filters.category && $filters.category !== '') {
			filtered = filtered.filter(event => event.category === $filters.category);
		}

		// Filter by date
		if ($filters.date) {
			filtered = filtered.filter(event => event.date === $filters.date);
		}

		// Filter by price
		if ($filters.priceRange === 'free') {
			filtered = filtered.filter(event => event.price === 0);
		} else if ($filters.priceRange === 'paid') {
			filtered = filtered.filter(event => event.price > 0);
		}

		// Filter by search term
		if ($filters.searchTerm) {
			const term = $filters.searchTerm.toLowerCase();
			filtered = filtered.filter(event => 
				event.title.toLowerCase().includes(term) ||
				event.description.toLowerCase().includes(term) ||
				event.location.toLowerCase().includes(term)
			);
		}

		return filtered;
	}
);

// Functions to manage events
export const addEvent = (event) => {
	events.update(currentEvents => {
		const newEvent = {
			...event,
			id: Date.now(), // Simple ID generation
			image: event.image || 'https://via.placeholder.com/400x200'
		};
		return [...currentEvents, newEvent];
	});
};

export const updateEvent = (id, updatedEvent) => {
	events.update(currentEvents => 
		currentEvents.map(event => 
			event.id === id ? { ...event, ...updatedEvent } : event
		)
	);
};

export const deleteEvent = (id) => {
	events.update(currentEvents => 
		currentEvents.filter(event => event.id !== id)
	);
};

// Functions to manage categories
export const addCategory = (category) => {
	categories.update(currentCategories => {
		const newCategory = {
			...category,
			id: category.id || category.name.toLowerCase().replace(/\s+/g, '-')
		};
		return [...currentCategories, newCategory];
	});
};

export const deleteCategory = (id) => {
	categories.update(currentCategories => 
		currentCategories.filter(category => category.id !== id)
	);
};

// Filter functions
export const setFilter = (filterType, value) => {
	filters.update(currentFilters => ({
		...currentFilters,
		[filterType]: value
	}));
};

export const clearFilters = () => {
	filters.set({
		category: '',
		date: '',
		priceRange: 'all',
		searchTerm: ''
	});
};
